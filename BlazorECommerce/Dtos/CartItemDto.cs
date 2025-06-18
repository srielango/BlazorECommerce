namespace BlazorECommerce.Dtos;

public class CartItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal TotalPrice => UnitPrice * Quantity;
    public string ImageUrl { get; set; } = default!;
}
