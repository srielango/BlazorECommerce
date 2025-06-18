using BlazorECommerce.Dtos;

namespace BlazorECommerce.Services
{
    public interface ICartService
    {
        Task<List<CartItemDto>> GetCartItemsAsync();
        Task AddToCartAsync(ProductDto item);
        Task ClearCartAsync();
        List<CartItemDto> GetCartItems();
        void RemoveFromCart(int productId);
        void IncrementQuantity(int productId);
        void DecrementQuantity(int productId);
    }
}
