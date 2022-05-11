namespace AAL.Accounts.Model
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Account Type entity
    /// </summary>
    /// <remarks>
    /// This entity type is not persisted as it is derived from the Account balance.
    /// </remarks>
    public class AccountType
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the threshold.
        /// </summary>
        [JsonIgnore]
        public int? Threshold { get; set; }
    }
}