namespace AAL.Accounts.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AAL.Accounts.Model;
    using AAL.Accounts.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Account Endpoints
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        /// <exception cref="ArgumentNullException">accountService</exception>
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        /// <summary>
        /// Gets the accounts.
        /// </summary>
        /// <param name="accountTypeId">The account type identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("accounts")]
        public async Task<IEnumerable<Account>> GetAccountsAsync(Guid? accountTypeId = null)
        {
            return await this.accountService.GetAccountsAsync(accountTypeId);
        }

        /// <summary>
        /// Creates an account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">account</exception>
        [HttpPost]
        [Route("accounts")]
        public async Task<Account> CreateAccountAsync([FromBody] Account account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return await this.accountService.CreateAsync(account);
        }

        /// <summary>
        /// Updates an account.
        /// </summary>
        /// <param name="id">The Id of the Account.</param>
        /// <param name="account">The account payload.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException">account</exception>
        [HttpPatch]
        [Route("accounts/{id}")]
        public async Task<Account> UpdateAccountAsync(Guid id, [FromBody] Account account)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(id)} cannot be empty.");
            }

            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            account.Id = id;
            return await this.accountService.UpdateAccountAsync(account);

        }

        /// <summary>
        /// Gets the account types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("accountTypes")]
        public async Task<IEnumerable<AccountType>> GetAccountTypesAsync()
        {
            return await this.accountService.GetAccountTypesAsync();
        }
    }
}