namespace AAL.Accounts.Helpers
{
    using System;
    using System.Collections.Generic;
    using AAL.Accounts.Model;

    /// <summary>
    /// Static definition of Account Types and their balance thresholds.
    /// </summary>
    public static class AccountTypes
    {
        /// <summary>
        /// All of the account types
        /// </summary>
        public static List<AccountType> All = new List<AccountType>
        {
            // ignore the fact that medals are in the wrong order!
            new AccountType { Id = Guid.Parse("d504a01f-3301-4625-8914-6677933813b2"), Name = "Silver", Threshold = 50000 },
            new AccountType { Id = Guid.Parse("f99ee345-7122-444a-b9ae-46ad565936b9"), Name = "Bronze", Threshold = 100000 },
            new AccountType { Id = Guid.Parse("870bd5d9-f843-48e7-8fd2-ac89d460f9c2"), Name = "Gold" },
            // TODO: Add any further account types here, e.g.
            // new AccountType { Id = Guid.Parse("dfc9329b-7d80-4114-bc00-8d860ad10a56"), Name = "Iron", Threshold = 10000 },
        };
    }
}