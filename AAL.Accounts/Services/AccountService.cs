namespace AAL.Accounts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AAL.Accounts.Data;
    using AAL.Accounts.Model;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Service providing methods to operate on <see cref="Account"/>
    /// </summary>
    /// <seealso cref="AAL.Accounts.Services.IAccountService" />
    public class AccountService : IAccountService
    {
        private readonly AccountContext accountContext;
        private readonly IAddressService addressService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="accountContext">The account context.</param>
        /// <param name="addressService">The address service.</param>
        /// <exception cref="ArgumentNullException">
        /// accountContext
        /// or
        /// addressService
        /// </exception>
        public AccountService(AccountContext accountContext, IAddressService addressService)
        {
            this.accountContext = accountContext ?? throw new ArgumentNullException(nameof(accountContext));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }


        /// <summary>
        /// Creates an account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">account</exception>
        public async Task<Account> CreateAsync(Account account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            this.accountContext.Accounts.Add(account);
            await this.accountContext.SaveChangesAsync();
            return account;
        }

        /// <summary>
        /// Gets all the accounts.
        /// </summary>
        /// <param name="accountTypeId">(Optional) The Id of the <see cref="AccountType" />.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Account>> GetAccountsAsync(Guid? accountTypeId)
        {
            Func<Account, bool> predicate = a => true;
            if (accountTypeId.HasValue)
            {
                predicate = a => a.AccountType != null && a.AccountType?.Id == accountTypeId;
            }

            // as we're using in-memory persistence, there is no performance loss in materialising the list
            var allAccounts = await this.accountContext.Accounts.ToListAsync();

            var results = allAccounts.Where(predicate)
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .ToList();

            // Add the address
            var address = await this.addressService.GetAddress();
            results.ForEach(r => r.Address = address);

            return await Task.FromResult(results);
        }

        /// <summary>
        /// Gets the account types.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AccountType>> GetAccountTypesAsync()
        {
            return await Task.FromResult(Helpers.AccountTypes.All);
        }

        /// <summary>
        /// Updates the account.
        /// </summary>
        /// <param name="updatedAccount">The updated account.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Cannot find {nameof(Account)} with {nameof(Account.Id)}: {updatedAccount.Id}.</exception>
        public async Task<Account> UpdateAccountAsync(Account updatedAccount)
        {
            var accountEntity = await this.accountContext.Accounts.FindAsync(updatedAccount.Id);
            if (accountEntity is null)
            {
                throw new KeyNotFoundException($"Cannot find {nameof(Account)} with {nameof(Account.Id)}: {updatedAccount.Id}.");
            }

            accountEntity.FirstName = updatedAccount.FirstName;
            accountEntity.LastName = updatedAccount.LastName;
            accountEntity.Balance = updatedAccount.Balance;
            await this.accountContext.SaveChangesAsync();
            return accountEntity;
        }
    }
}