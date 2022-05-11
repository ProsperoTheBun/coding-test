namespace AAL.Accounts.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AAL.Accounts.Controllers;
    using AAL.Accounts.Data;
    using AAL.Accounts.Model;
    using AAL.Accounts.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ApiTests
    {
        private readonly DbContextOptions<AccountContext> dbOptions =
          new DbContextOptionsBuilder<AccountContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        [TestInitialize]
        public void Init()
        {
            // seed the database
            using (var context = new AccountContext(this.dbOptions))
            {
                context.SeedData();
            }
        }

        [TestMethod]
        public async Task EndToEnd_ReturnsAllFullAccounts()
        {
            // Arrange
            using var accountContext = new AccountContext(this.dbOptions);
            var httpClientFactory = new TestHttpClientFactory();
            var addressService = new AddressService(httpClientFactory);
            var accountService = new AccountService(accountContext, addressService);
            var controller = new AccountController(accountService);

            // Act
            var result = await controller.GetAccountsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Account>));
            Assert.IsTrue(result.Any());
            Assert.IsNotNull(result.First().AccountType);
            Assert.IsNotNull(result.First().Address);
            Assert.IsFalse(string.IsNullOrEmpty(result.First().Address.City));
            Assert.IsFalse(string.IsNullOrEmpty(result.First().Address.Postcode));
        }
    }
}