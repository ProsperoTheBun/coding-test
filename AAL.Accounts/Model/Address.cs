namespace AAL.Accounts.Model
{
    using System;

    /// <summary>
    /// Address entity
    /// </summary>
    /// <remarks>
    /// This entity type is not persisted.
    /// </remarks>
    public class Address
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <remarks>
        /// Id is only included to satisfy EntityFrameworks rules.
        /// </remarks>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        public string Postcode { get; set; }
    }
}