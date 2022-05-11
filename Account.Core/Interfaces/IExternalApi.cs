namespace Account.Core.Interfaces
{
    /// <summary>
    /// Validate an amount against account type
    /// </summary>
    public interface IExternalApi
    {
        /// <summary>
        /// Checks the account balance.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="accountType">Type of the account.</param>
        bool CheckAccountBalance(decimal amount, AccountType accountType);
    }
}