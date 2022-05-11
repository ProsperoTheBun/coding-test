namespace AAL.Accounts.Services
{
    using System.Threading.Tasks;
    using AAL.Accounts.Model;

    /// <summary>
    /// Service to provide addresses.
    /// </summary>
    public interface IAddressService
    {
        /// <summary>
        /// Gets an address.
        /// </summary>
        /// <returns></returns>
        Task<Address> GetAddress();
    }
}