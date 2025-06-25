using BlazorECommerce.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace BlazorECommerce.Services;

public class OrderService
{
    private readonly ApiClient _apiClient;
    private readonly CustomAuthenticationStateProvider _authProvider;

    public OrderService(ApiClient apiClient, AuthenticationStateProvider authProvider)
    {
        _apiClient = apiClient;
        _authProvider = (CustomAuthenticationStateProvider)authProvider;
    }

    private async Task<string?> GetCurrentUserNameAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        return authState.User.Identity?.Name;
    }

    public async Task<List<OrderDto>> GetOrdersAsync()
    {
        var userName = await GetCurrentUserNameAsync();
        if (string.IsNullOrEmpty(userName))
            return new List<OrderDto>();

        return await _apiClient.GetAsync<List<OrderDto>>($"order/{userName}", await GetAccessTokenAsync()) ?? new List<OrderDto>();
    }

    public async Task CheckoutOrderAsync(OrderDto order)
    {
        await _apiClient.PostAsync("order", order, await GetAccessTokenAsync());
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        await _apiClient.DeleteAsync($"order/{orderId}", await GetAccessTokenAsync());
    }

    private async Task<string?> GetAccessTokenAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity?.IsAuthenticated == true)
        {
            return _authProvider.AccessToken;
        }
        return null;
    }
}
