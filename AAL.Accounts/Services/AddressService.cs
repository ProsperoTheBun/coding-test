namespace AAL.Accounts.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AAL.Accounts.Model;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Service to provide addresses.
    /// </summary>
    /// /// <seealso cref="AAL.Accounts.Services.IAddressService" />
    public class AddressService : IAddressService
    {
        private const string addressRequestUrl = "https://randomuser.me/api/?nat=gb";
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressService"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <exception cref="ArgumentNullException">httpClientFactory</exception>
        public AddressService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// Gets an address.
        /// </summary>
        /// <returns></returns>
        public async Task<Address> GetAddress()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, addressRequestUrl);
            request.Headers.Add("Accept", "application/json");
            var client = this.httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(content);
                var city = (string)jsonObject.SelectToken("$.results[0].location.city");
                var postcode = (string)jsonObject.SelectToken("$.results[0].location.postcode");

                return new Address
                {
                    City = city,
                    Postcode = postcode,
                };
            }
            return null;
        }
    }
}