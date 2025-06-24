using BlazorECommerce.Dtos;

namespace BlazorECommerce.Services
{
    public interface ICartService
    {
        Task<List<CartItemDto>> GetCartItemsAsync();
        Task AddToCartAsync(ProductDto item);
        Task ClearCartAsync();
        List<CartItemDto> GetCartItems();
        Task RemoveFromCartAsync(string productName);
        Task IncrementQuantity(string productName);
        Task DecrementQuantity(string productName);
    }
}
