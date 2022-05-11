namespace AAL.Accounts.Model
{
    using System;
    using AAL.Accounts.Helpers;

    /// <summary>
    /// Account entity
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets the type of the account.
        /// </summary>
        public AccountType AccountType
        {
            get
            {
                return AccountTypeCalculator.GetAccountType(this.Balance);
            }
        }
    }
}