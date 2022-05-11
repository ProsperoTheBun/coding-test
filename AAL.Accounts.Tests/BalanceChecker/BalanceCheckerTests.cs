namespace AAL.Accounts.Tests.BalanceChecker
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Account.Core;
    using Moq;
    using Account.Core.Interfaces;

    [TestClass]
    public class BalanceCheckerTests
    {
        private readonly Mock<IPersistence> persistence = new Mock<IPersistence>();
        private readonly Mock<IExternalApi> externalApi = new Mock<IExternalApi>();

        // NB: tests for ArgumentNullException omitted due to time considerations.

        [TestMethod]
        public void Process_HighBalance_CallsExternalApi()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();
            var amount = 1000001;
            var accountType = AccountType.Gold;
            this.externalApi
                .Setup(x => x.CheckAccountBalance(It.IsAny<decimal>(), It.IsAny<AccountType>()));

            // Act
            sut.Process(amount, this.persistence.Object, this.externalApi.Object, accountType);

            // Assert          
            this.externalApi.Verify(x => x.CheckAccountBalance(amount, accountType));
        }

        [TestMethod]
        public void Process_ExternalApiResultIsTrue_ReturnsTrue()
        {
            // Arrange
            var sut = this.GetSystemUnderTest();
            var amount = 1000001;
            var accountType = AccountType.Gold;
            this.externalApi
                .Setup(x => x.CheckAccountBalance(It.IsAny<decimal>(), It.IsAny<AccountType>()))
                .Returns(true);

            // Act
            var result = sut.Process(amount, this.persistence.Object, this.externalApi.Object, accountType);

            // Assert          
            Assert.IsTrue(result);
        }

        private BalanceChecker GetSystemUnderTest()
        {
            return new BalanceChecker();
        }
    }
}
