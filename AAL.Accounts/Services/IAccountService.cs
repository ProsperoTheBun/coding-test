namespace AAL.Accounts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AAL.Accounts.Model;

    /// <summary>
    /// Service providing methods to operate on <see cref="Account"/>
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Gets all the accounts.
        /// </summary>
        /// <param name="accountTypeId">(Optional) The Id of the <see cref="AccountType"/>.</param>
        /// <returns></returns>
        Task<IEnumerable<Account>> GetAccountsAsync(Guid? accountTypeId);

        /// <summary>
        /// Creates an account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        Task<Account> CreateAsync(Account account);

        /// <summary>
        /// Updates the account.
        /// </summary>
        /// <param name="updatedAccount">The updated account.</param>
        /// <returns></returns>
        Task<Account> UpdateAccountAsync(Account updatedAccount);

        /// <summary>
        /// Gets the account types.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AccountType>> GetAccountTypesAsync();
    }
}