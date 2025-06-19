using System.Diagnostics.Eventing.Reader;

namespace BlazorECommerce.Services
{
    public class UserService
    {
        public string? CurrentUser { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUser);

        public bool Login(string username, string password)
        {
            // Simulate a login process
            if (username == "testuser" && password == "password123")
            {
                CurrentUser = username;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
