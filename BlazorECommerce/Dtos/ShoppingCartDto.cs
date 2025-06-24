namespace BlazorECommerce.Dtos
{
    public class ShoppingCartDto
    {
        public string UserName { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }
}
