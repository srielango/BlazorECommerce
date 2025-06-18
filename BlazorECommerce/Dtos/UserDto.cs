namespace BlazorECommerce.Dtos
{
    public class UserDto
    {
        public string Email { get; set; } = default!;
        public string Role { get; set; } = "Customer"; // Default role is Customer
        public string Token { get; set; } = string.Empty; //JWT token for authentication
    }
}
