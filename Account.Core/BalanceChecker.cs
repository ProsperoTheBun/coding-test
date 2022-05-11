namespace Account.Core
{
    using System;
    using Account.Core.Interfaces;

    /// <summary>
    /// Check if a balance can be saved.
    /// </summary>
    /// <seealso cref="Account.Core.Interfaces.IBalanceChecker" />
    public class BalanceChecker : IBalanceChecker
    {
        // Always OK to save balances below this
        private const int LowerAccountThreshold = 10;

        // Balances above this amount submitted after the day of the month specified by DayOfMonthThreshold
        // will be validated by IPersistence.
        private const int MidMonthAccountThreshold = 50;

        private const int DayOfMonthThreshold = 15;

        // Balances above this amount will be validated by IExternalApi
        private const int UpperAccountThreshold = 100000;

        /// <summary>
        /// Processes the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="persistence">The persistence mechanism.</param>
        /// <param name="externalApi">The external API.</param>
        /// <param name="accountType">Type of the account.</param>
        public bool Process(decimal amount, IPersistence persistence, IExternalApi externalApi, AccountType accountType)
        {
            if (amount < LowerAccountThreshold)
            {
                Console.WriteLine($"less than {LowerAccountThreshold}");
                return true;
            }

            if (persistence is null)
            {
                throw new ArgumentNullException(nameof(persistence));
            }

            if (amount > MidMonthAccountThreshold && DateTime.Now.Day > DayOfMonthThreshold)
            {
                return persistence.GetInfo();
            }

            if (externalApi is null)
            {
                throw new ArgumentNullException(nameof(externalApi));
            }

            if (amount > UpperAccountThreshold)
            {
                return externalApi.CheckAccountBalance(amount, accountType);
            }

            return true;
        }
    }
}