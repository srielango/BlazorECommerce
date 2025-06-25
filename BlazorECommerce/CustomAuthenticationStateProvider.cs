using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Blazored.LocalStorage;

namespace BlazorECommerce
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _user = new(new ClaimsIdentity());
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public string? AccessToken { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(AccessToken);
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedUser = await _localStorage.GetItemAsync<string>("authUser");
            var savedToken = await _localStorage.GetItemAsync<string>("accessToken");

            if (!string.IsNullOrEmpty(savedUser) && !string.IsNullOrEmpty(savedToken))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, savedUser),
                }, "apiauth_type");

                _user = new ClaimsPrincipal(identity);
                AccessToken = savedToken;
            }
            else
            {
                _user = new ClaimsPrincipal(new ClaimsIdentity());
                AccessToken = null;
            }

            return new AuthenticationState(_user);
        }

        public async Task MarkUserAsAuthenticated(string userName, string accessToken)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName),
            }, "apiauth_type");

            _user = new ClaimsPrincipal(identity);
            AccessToken = accessToken;

            await _localStorage.SetItemAsync("authUser", userName);
            await _localStorage.SetItemAsync("accessToken", accessToken);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity());
            AccessToken = null;

            await _localStorage.RemoveItemAsync("authUser");
            await _localStorage.RemoveItemAsync("accessToken");

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
        }
    }
}
