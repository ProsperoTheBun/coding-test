namespace Account.Core
{
    using Account.Core.Interfaces;

    /// <summary>
    /// This implementation of <see cref="IPersistence"/> will always return true.
    /// </summary>
    public class Persistence : IPersistence
    {
        /// <summary>
        /// Gets the information.
        /// </summary>
        public bool GetInfo() => true;
    }
}