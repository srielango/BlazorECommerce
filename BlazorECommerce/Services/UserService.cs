using BlazorECommerce.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;

namespace BlazorECommerce.Services
{
    public class UserService
    {
        private readonly ApiClient _apiClient;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
        public string? CurrentUser { get; private set; }
        public UserService(ApiClient apiClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _apiClient = apiClient;
            _authenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var response = await _apiClient.PostRawAsync("api/auth/register", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var response = await _apiClient.GetAsync<JsonElement>($"api/auth/login?email={email}&password={password}");
            var token = response.GetProperty("accessToken").GetString();
            await _authenticationStateProvider.MarkUserAsAuthenticated(email, token!);
            CurrentUser = email;
            return true;
        }

        public async Task Logout()
        {
            CurrentUser = null;
            await _apiClient.DeleteAsync($"basketapi/basket/{CurrentUser}");
            await _authenticationStateProvider.MarkUserAsLoggedOut();
        }
    }
}
