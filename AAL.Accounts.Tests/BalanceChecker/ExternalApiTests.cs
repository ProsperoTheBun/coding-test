namespace AAL.Accounts.Tests.BalanceChecker
{
    using Account.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class ExternalApiTests
    {
        [TestMethod]
        public void CheckAccountBalance_BalanceIsAboveThreshold_AccountTypeIsGold_ReturnsTrue()
            => this.AssertActualIsExpected(1000001, AccountType.Gold, true);

        [TestMethod]
        public void CheckAccountBalance_BalanceBelowThreshold_AccountTypeIsGold_ReturnsFalse()
            => this.AssertActualIsExpected(999999, AccountType.Gold, false);

        [TestMethod]
        public void CheckAccountBalance_BalanceEqualsThreshold_AccountTypeIsGold_ReturnsFalse()
            => this.AssertActualIsExpected(1000000, AccountType.Gold, false);

        [TestMethod]
        public void CheckAccountBalance_BalanceBelowThreshold_AccountTypeIsNotGold_ReturnsFalse()
            => this.AssertActualIsExpected(999999, AccountType.Silver, false);

        private void AssertActualIsExpected(decimal amount, AccountType accountType, bool expectedResult)
        {
            // NB: I would normally use DataTestMethod to assert all these combinations in one method,
            // but there seems to be some bug in the MSTestAdapter with .Net Core 3.1

            // Arrange
            var sut = new ExternalApi();

            // Act
            var result = sut.CheckAccountBalance(amount, accountType);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}