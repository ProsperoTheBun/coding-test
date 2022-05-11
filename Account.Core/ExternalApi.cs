namespace Account.Core
{
    using Account.Core.Interfaces;

    /// <summary>
    /// Validate an amount against account type
    /// </summary>
    public class ExternalApi : IExternalApi
    {
        // Balances can only be saved if above this amount
        private const int AccountThreshold = 1000000;

        /// <summary>
        /// Checks the account balance.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="accountType">Type of the account.</param>
        public bool CheckAccountBalance(decimal amount, AccountType accountType) => amount > AccountThreshold && accountType == AccountType.Gold;
    }
}