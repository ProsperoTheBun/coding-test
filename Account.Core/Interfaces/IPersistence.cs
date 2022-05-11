namespace Account.Core.Interfaces
{
    /// <summary>
    /// Validate an amount against the persistence mechanism.
    /// </summary>
    public interface IPersistence
    {
        /// <summary>
        /// Gets the information.
        /// </summary>
        bool GetInfo();
    }
}