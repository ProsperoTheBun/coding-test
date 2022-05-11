namespace AAL.Accounts.Tests.BalanceChecker
{
    using Account.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PersistenceTests
    {
        [TestMethod]
        public void GetInfo_ReturnsTrue()
        {
            // Arrange
            var sut = new Persistence();

            // Act
            var result = sut.GetInfo();

            // Assert
            Assert.IsTrue(result);
        }
    }
}