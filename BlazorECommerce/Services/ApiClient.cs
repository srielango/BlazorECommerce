using System.Net.Http.Headers;

namespace BlazorECommerce.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private string? _accessToken;

        public void SetAccessToken(string token)
        {
            _accessToken = token;
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<T> PostAsync<T>(string url, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<HttpResponseMessage> PostRawAsync<T>(string url, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
