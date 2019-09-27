using System.Net.Http;

namespace Walletico.Shared
{
    public static class HttpClientSingleton
    {
        private static readonly  HttpClient _httpClient;
        static HttpClientSingleton()
        {
            _httpClient = new HttpClient();
        }

        public static HttpClient HttpClient => _httpClient;
    }
}
