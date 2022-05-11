namespace AAL.Accounts.Helpers
{
    using System.Linq;
    using AAL.Accounts.Model;

    /// <summary>
    /// Utility class to calculate which account type a balance belongs to.
    /// </summary>
    public static class AccountTypeCalculator
    {
        /// <summary>
        /// Gets the type of the account.
        /// </summary>
        /// <param name="balance">The balance.</param>
        /// <returns></returns>
        public static AccountType GetAccountType(int balance)
        {
            foreach (var accountType in AccountTypes.All.Where(t => t.Threshold.HasValue).OrderBy(t => t.Threshold))
            {
                if (balance < accountType.Threshold)
                {
                    return accountType;
                }
            }

            // balance is over the maximum threshold so return the type with no threshold
            return AccountTypes.All.FirstOrDefault(t => !t.Threshold.HasValue);
        }
    }
}