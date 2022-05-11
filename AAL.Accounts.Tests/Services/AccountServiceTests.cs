namespace AAL.Accounts.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AAL.Accounts.Data;
    using AAL.Accounts.Helpers;
    using AAL.Accounts.Model;
    using AAL.Accounts.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AccountServiceTests
    {
        private readonly Mock<IAddressService> addressService = new Mock<IAddressService>();

        private readonly IEnumerable<Account> store = new[] {
            new Account { Id = Guid.NewGuid(), LastName = "Aaaaaa", Balance = 25000 },
            new Account { Id = Guid.NewGuid(), LastName = "Bbbbbb", Balance = 75000 },
            new Account { Id = Guid.NewGuid(), LastName = "Cccccc", Balance = 75000 },
        };

        private AccountContext accountContext;

        [TestInitialize]
        public void Init()
        {
            // using a GUID for the database name ensures a fresh DB instance for each test thread.
            this.accountContext = new AccountContext(new DbContextOptionsBuilder<AccountContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

            // seed data
            this.accountContext.Accounts.AddRange(store);
            this.accountContext.SaveChanges();
        }

        [TestMethod]
        public void ctor_NullArguments_Throws()
        {
            // Arrange
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new AccountService(null, null);
            });
        }

        [TestMethod]
        public async Task CreateAsync_ValidAccount_ReturnsNewAccount()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();
            const string lastName = "Zzzzz";
            Account newAccount = new Account
            {
                LastName = lastName,
            };

            // Act
            var result = await sut.CreateAsync(newAccount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Id);
            Assert.AreEqual(lastName, result.LastName);
            Assert.AreEqual(this.store.Count() + 1, this.accountContext.Accounts.Count());
        }

        [TestMethod]
        public async Task GetAccountsAsync_NoFilter_ReturnsAllAccounts()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();

            // Act
            var result = await sut.GetAccountsAsync(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(store.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetAccountsAsync_NoFilter_IncludesAddress()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();
            this.addressService
                .Setup(x => x.GetAddress())
                .ReturnsAsync(new Address());

            // Act
            var result = await sut.GetAccountsAsync(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(store.Count(), result.Count());
            this.addressService.Verify(x => x.GetAddress(), Times.Once);
        }

        [TestMethod]
        public async Task GetAccountsAsync_WithFilter_ReturnsFilteredAccounts()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();
            var filterId = Guid.NewGuid();

            var result = await sut.GetAccountsAsync(AccountTypes.All.Single(t=>t.Name == "Bronze").Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        // Other test cases omitted for time

        private AccountService GetSystemUnderTest()
        {
            return new AccountService(this.accountContext, this.addressService.Object);
        }
    }
}