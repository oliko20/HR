using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HR.UI
{
    public class HttpClientWrapper
    {
        private readonly string _baseUrl;

        public HttpClientWrapper(IConfiguration configuration)
        {
            _baseUrl = configuration["ApiUrl"];
        }
        public async Task<T> GetAsync<T>(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        public async Task DeleteAsync(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            var result = await client.DeleteAsync(url);
            result.EnsureSuccessStatusCode();
        }

        public async Task<HttpStatusCode> PostAsync<T>(string url, T body)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            var result = await client.PostAsJsonAsync(url, body);
            return result.StatusCode;
        }

        public async Task<T> PutAsync<T>(string url, T body)
        {
            var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            var response = await client.PutAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }
    }
}
