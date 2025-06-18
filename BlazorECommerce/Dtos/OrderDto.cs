namespace BlazorECommerce.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    public string UserId { get; set; } = default!;
    public decimal TotalAmount => Items.Sum(item => item.TotalPrice);
    public DateTime PlacedOn { get; set; }
    public string Status { get; set; } = "Pending";
}
