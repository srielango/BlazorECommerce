﻿namespace BlazorECommerce.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Summary { get; set; } = default!;
        public decimal Price { get; set; }
        public string ImageFile { get; set; } = default!;
    }
}
