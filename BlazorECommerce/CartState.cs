using Microsoft.AspNetCore.Components;

namespace BlazorECommerce
{
    public class CartState
    {
        public event Func<Task>? OnChange;

        public Task NotifyStateChanged()
        {
            return OnChange?.Invoke() ?? Task.CompletedTask;
        }
    }
}
