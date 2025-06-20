using BlazorECommerce.Dtos;
using BlazorECommerce.Models;

namespace BlazorECommerce.Services
{
    public class UserService
    {
        private readonly Dictionary<string, UserDto> _users = new();

        public string? CurrentUser { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUser);

        public Task<bool> RegisterAsync(RegisterRequest request)
        {
            if(_users.ContainsKey(request.Email))
                return Task.FromResult(false);

            var user = new UserDto
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                Role = "Customer",
                Token = Guid.NewGuid().ToString()
            };

            _users[request.Email] = user;
            return Task.FromResult(true);
        }

        public Task<bool> LoginAsync(string username, string password)
        {
            return Task.FromResult(_users.TryGetValue(username, out var user) && user.Password == password && (CurrentUser = user.Name) != null);
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
