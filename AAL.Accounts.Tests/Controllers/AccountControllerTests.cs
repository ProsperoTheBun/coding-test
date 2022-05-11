namespace AAL.Accounts.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AAL.Accounts.Controllers;
    using AAL.Accounts.Model;
    using AAL.Accounts.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> mockAccountService = new Mock<IAccountService>();

        private readonly IEnumerable<Account> store = new[] {
            new Account { Id = Guid.NewGuid() },
            new Account { Id = Guid.NewGuid() },
            new Account { Id = Guid.NewGuid() },
        };

        private readonly IEnumerable<AccountType> accountTypes = new[] {
            new AccountType(),
            new AccountType(),
            new AccountType(),
        };

        [TestInitialize]
        public void Init()
        {
            mockAccountService.Setup(x => x.GetAccountsAsync(null)).ReturnsAsync(store);
            mockAccountService.Setup(x => x.GetAccountsAsync(It.IsAny<Guid?>())).ReturnsAsync(store.Take(2));
        }

        [TestMethod]
        public void ctor_NullService_Throws()
        {
            // Arrange
            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new AccountController(null);
            });
        }

        [TestMethod]
        public async Task GetAccountsAsync_NoFilter_ReturnsAllAccounts()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();

            // Act
            var result = await sut.GetAccountsAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
            this.mockAccountService.Verify(x => x.GetAccountsAsync(null));
        }

        [TestMethod]
        public async Task GetAccountsAsync_WithFilter_ReturnsFilteredAccounts()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();
            var accountTypeId = Guid.NewGuid();

            // Act
            var result = await sut.GetAccountsAsync(accountTypeId);

            // Assert
            Assert.AreEqual(2, result.Count());
            this.mockAccountService.Verify(x => x.GetAccountsAsync(accountTypeId));
        }

        [TestMethod]
        public async Task CreateAccountAsync_ValidAccount_ReturnsNewlyCreatedEntity()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();
            var newAccount = new Account
            {
                FirstName = "first",
                LastName = "last",
                Balance = 1234
            };

            this.mockAccountService
                .Setup(x => x.CreateAsync(It.IsAny<Account>()))
                .ReturnsAsync(() =>
                {
                    newAccount.Id = Guid.NewGuid();
                    return newAccount;
                });

            // Act
            var result = await sut.CreateAccountAsync(newAccount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Id);
            Assert.AreEqual("first", result.FirstName);
            Assert.AreEqual("last", result.LastName);
            Assert.AreEqual(1234, result.Balance);
            this.mockAccountService.Verify(x => x.CreateAsync(newAccount));
        }

        [TestMethod]
        public async Task CreateAccountAsync_NullAccount_Throws()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();
            // Act
            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => { await sut.CreateAccountAsync(null); });
        }

        [TestMethod]
        public async Task UpdateAccountAsync_ExistingAccount_ReturnsUpdatedAccount()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();
            var updatedAccount = new Account
            {
                Id = Guid.NewGuid()
            };

            this.mockAccountService
                .Setup(x => x.UpdateAccountAsync(It.IsAny<Account>()))
                .ReturnsAsync((Account o) => new Account { Id = o.Id });

            // Act
            var result = await sut.UpdateAccountAsync(updatedAccount.Id, updatedAccount);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedAccount.Id, result.Id);
            this.mockAccountService.Verify(x => x.UpdateAccountAsync(updatedAccount));
        }

        [TestMethod]
        public async Task GetAccountTypes_ReturnsResults()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();

            this.mockAccountService
                .Setup(x => x.GetAccountTypesAsync())
                .ReturnsAsync(this.accountTypes);

            // Act
            var result = await sut.GetAccountTypesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            this.mockAccountService.Verify(x => x.GetAccountTypesAsync());
        }

        private AccountController GetSystemUnderTest()
        {
            return new AccountController(mockAccountService.Object);
        }
    }
}