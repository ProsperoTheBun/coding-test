namespace AAL.Accounts.Tests.IntegrationTests
{
    using System;
    using System.Net.Http;

    internal sealed class TestHttpClientFactory : IHttpClientFactory
    {
        private static readonly Lazy<HttpClient> lazyHttpClient = new Lazy<HttpClient>(() => new HttpClient());

        public HttpClient CreateClient(string name)
        {
            return lazyHttpClient.Value;
        }
    }
}