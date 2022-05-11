namespace AAL.Accounts.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AAL.Accounts.Helpers;
    using AAL.Accounts.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AccountTypesTests
    {
        [TestMethod]
        public void AccountTypes_GetAll_EnsureMaximumAccountExists()
        {
            // Assert
            // there must always be an account type with null threshold
            Assert.IsTrue(AccountTypes.All.Any(t => !t.Threshold.HasValue));
        }

        [TestMethod]
        public void AccountTypeCalculator_ValidListOfTypes_ReturnsExpectedType()
        {
            // Act
            var result1 = AccountTypeCalculator.GetAccountType(0);
            var result2 = AccountTypeCalculator.GetAccountType(50000);
            var result3 = AccountTypeCalculator.GetAccountType(99999);
            var result4 = AccountTypeCalculator.GetAccountType(100001);

            // Assert
            Assert.AreEqual("Silver", result1.Name);
            Assert.AreEqual("Bronze", result2.Name);
            Assert.AreEqual("Bronze", result3.Name);
            Assert.AreEqual("Gold", result4.Name);
        }
    }
}