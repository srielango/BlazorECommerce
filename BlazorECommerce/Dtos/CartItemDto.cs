namespace BlazorECommerce.Dtos;

public class CartItemDto
{
    public string UserName { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal TotalPrice => Price * Quantity;
    public string ImageUrl { get; set; } = default!;
}
