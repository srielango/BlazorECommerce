using BlazorECommerce.Dtos;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorECommerce.Services;

public class CartService : ICartService
{
    private readonly CartState _cartState;
    private readonly ApiClient _apiClient;
    private readonly CustomAuthenticationStateProvider _authProvider;
    private readonly OrderService _orderService;

    private string _userName = string.Empty;

    public CartService(CartState cartState, ApiClient apiClient, AuthenticationStateProvider authProvider, OrderService orderService)
    {
        _cartState = cartState;
        _apiClient = apiClient;
        _authProvider = (CustomAuthenticationStateProvider)authProvider;
        _orderService = orderService;
    }

    public async Task<List<CartItemDto>> GetCartItemsAsync()
    {
        await EnsureUserNameAsync();
        var cart = await GetCartAsync();
        return cart?.Items ?? new List<CartItemDto>();
    }
    public List<CartItemDto> GetCartItems()
    {
        var task = GetCartItemsAsync();
        return task.Result;
    }

    public async Task AddToCartAsync(ProductDto item)
    {
        await EnsureUserNameAsync();
        var cart = await GetOrCreateCartAsync();

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductName == item.Name);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cart.Items.Add(new CartItemDto
            {
                ProductName = item.Name,
                Price = item.Price,
                Quantity = 1,
                ImageUrl = item.ImageFile
            });
        }

        await _apiClient.PostAsync($"basketapi/basket/{_userName}", cart, await GetAccessTokenAsync());
        await _cartState.NotifyStateChanged();
    }

    public async Task ClearCartAsync()
    {
        await EnsureUserNameAsync();
        await _apiClient.DeleteAsync($"basketapi/basket/{_userName}", await GetAccessTokenAsync());
        await _cartState.NotifyStateChanged();
    }

    public async Task RemoveFromCartAsync(string productName)
    {
        var cart = await GetCartAsync();
        var item = cart.Items.FirstOrDefault(i => i.ProductName == productName);
        if (item != null)
        {
            cart.Items.Remove(item);
        }

        await _apiClient.PostAsync($"basketapi/basket/{_userName}", cart, await GetAccessTokenAsync());
        await _cartState.NotifyStateChanged();
    }

    public async Task IncrementQuantity(string productName)
    {
        var cart = await GetCartAsync();
        var item = cart.Items.FirstOrDefault(i => i.ProductName == productName);
        if (item != null)
        {
            item.Quantity++;
        }
        await _apiClient.PostAsync($"basketapi/basket/{_userName}", cart, await GetAccessTokenAsync());
        await _cartState.NotifyStateChanged();
    }

    public async Task DecrementQuantity(string productName)
    {
        var cart = await GetCartAsync();
        var item = cart.Items.FirstOrDefault(i => i.ProductName == productName);

        if (item != null)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                cart.Items.Remove(item);
            }
        }
        await _apiClient.PostAsync($"basketapi/basket/{_userName}", cart, await GetAccessTokenAsync());
        await _cartState.NotifyStateChanged();

    }

    public async Task PlaceOrderAsync()
    {
        await EnsureUserNameAsync();
        var cartItems = await GetCartItemsAsync();

        var order = new OrderDto
        {
            UserName = _userName,
            TotalPrice = cartItems.Sum(x => x.Price * x.Quantity)
        };

        await _orderService.CheckoutOrderAsync(order);
        await ClearCartAsync();
    }


    private async Task<string?> GetAccessTokenAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        if(authState.User.Identity?.IsAuthenticated == true)
        {
            return _authProvider.AccessToken;
        }
        return null;
    }


    private async Task<ShoppingCartDto> GetOrCreateCartAsync()
    {
        return await GetCartAsync() ?? new ShoppingCartDto
        {
            UserName = _userName,
            Items = new List<CartItemDto>()
        };
    }

    private async Task<ShoppingCartDto> GetCartAsync()
    {
        await EnsureUserNameAsync();
        var cart = await _apiClient.GetAsync<ShoppingCartDto>($"basketapi/basket/{_userName}", await GetAccessTokenAsync());
        return cart ?? new ShoppingCartDto { UserName = _userName, Items = new List<CartItemDto>() };
    }
    private async Task EnsureUserNameAsync()
    {
        if (string.IsNullOrEmpty(_userName))
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            _userName = authState.User.Identity?.Name ?? string.Empty;
        }
        if(string.IsNullOrEmpty(_userName))
        {
            throw new InvalidOperationException("User is not authenticated.");
        }
    }
}