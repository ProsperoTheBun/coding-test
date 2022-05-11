namespace Account.Core.Interfaces
{
    /// <summary>
    /// Check if a balance can be saved.
    /// </summary>
    public interface IBalanceChecker
    {
        /// <summary>
        /// Processes the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="persistence">The persistence.</param>
        /// <param name="externalApi">The external API.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns></returns>
        bool Process(decimal amount, IPersistence persistence, IExternalApi externalApi, AccountType accountType);
    }
}