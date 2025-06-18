using BlazorECommerce.Dtos;

namespace BlazorECommerce.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
}
