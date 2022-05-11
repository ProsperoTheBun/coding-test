namespace AAL.Accounts.Data
{
    using AAL.Accounts.Model;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Database context for accounts
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AccountContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Seeds some data for initial use.
        /// </summary>
        public void SeedData()
        {
            this.Accounts.AddRange(new[]
            {
                new Account { FirstName = "Franklin", LastName = "Alvarez", Balance = 23000 },
                new Account { FirstName = "Armando", LastName = "Holland", Balance = 78100 },
                new Account { FirstName = "Holly", LastName = "Brooks", Balance = 138000 }
            });
            this.SaveChanges();
        }
    }
}