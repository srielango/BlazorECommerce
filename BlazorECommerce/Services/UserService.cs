using BlazorECommerce.Models;
using System.Text.Json;

namespace BlazorECommerce.Services
{
    public class UserService
    {
        //private readonly Dictionary<string, UserDto> _users = new();

        private readonly ApiClient _apiClient;
        private readonly ICartService _cartService;
        public UserService(ApiClient apiClient, ICartService cartService)
        {
            _apiClient = apiClient;
            _cartService = cartService;
        }

        public string? CurrentUser { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUser);

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var response = await _apiClient.PostRawAsync("api/auth/register", request);
            return response.IsSuccessStatusCode;

            //if(_users.ContainsKey(request.Email))
            //    return Task.FromResult(false);

            //var user = new UserDto
            //{
            //    Email = request.Email,
            //    Name = request.Name,
            //    Password = request.Password,
            //    Role = "Customer",
            //    Token = Guid.NewGuid().ToString()
            //};

            //_users[request.Email] = user;
            //return Task.FromResult(true);
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var response = await _apiClient.GetAsync<JsonElement>($"api/auth/login?email={email}&password={password}");
            var token = response.GetProperty("accessToken").GetString();

            _apiClient.SetAccessToken(token);
            CurrentUser = email;
            return true;
        }

        public async Task Logout()
        {
            CurrentUser = null;
            await _cartService.ClearCartAsync();
            _apiClient.SetAccessToken(null);
        }
    }
}
