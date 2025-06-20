using BlazorECommerce.Dtos;

namespace BlazorECommerce.Services;

public class CartService : ICartService
{
    private readonly List<CartItemDto> _cartItems = new();
    private readonly CartState _cartState;

    public CartService(CartState cartState)
    {
        _cartState = cartState;
    }

    public Task<List<CartItemDto>> GetCartItemsAsync()
    {
        return Task.FromResult(_cartItems);
    }

    public List<CartItemDto> GetCartItems()
    {
        return _cartItems;
    }

    public Task AddToCartAsync(ProductDto item)
    {
        var existingItem = _cartItems.FirstOrDefault(i => i.ProductId == item.Id);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            _cartItems.Add(new CartItemDto
            {
                ProductId = item.Id,
                ProductName = item.Name,
                UnitPrice = item.Price,
                Quantity = 1,
                ImageUrl = item.ImageUrl
            });
        }

        _cartState.NotifyStateChanged();

        return Task.CompletedTask;
    }
    public Task ClearCartAsync()
    {
        _cartItems.Clear();
        _cartState.NotifyStateChanged();
        return Task.CompletedTask;
    }

    public void RemoveFromCart(int productId)
    {
        var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            _cartItems.Remove(item);
        }
        _cartState.NotifyStateChanged();
    }

    public void IncrementQuantity(int productId)
    {
        var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            item.Quantity++;
        }
        _cartState.NotifyStateChanged();
    }

    public void DecrementQuantity(int productId)
    {
        var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                _cartItems.Remove(item);
            }
            _cartState.NotifyStateChanged();
        }
    }
}