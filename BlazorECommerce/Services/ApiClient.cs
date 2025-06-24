using System.Net.Http.Headers;

namespace BlazorECommerce.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<T> GetAsync<T>(string url, string? accessToken = null)
        {
           _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<T> PostAsync<T>(string url, T data, string? accessToken = null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<HttpResponseMessage> PostRawAsync<T>(string url, T data, string? accessToken = null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T data, string? accessToken = null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url, string? accessToken = null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
