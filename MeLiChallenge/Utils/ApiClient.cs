using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeLiChallenge.Utils
{
    public partial class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<T> GetAsync<T>(Uri uri)
        {
            var response = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T> GetAsync<T>(string uri, string queryString = "")
        {
            var requestUrl = CreateRequestUri(uri, queryString);

            return await GetAsync<T>(requestUrl);
        }

        private Uri CreateRequestUri(string uri, string queryString = "")
        {
            var endpoint = new Uri(uri);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }
    }
}
