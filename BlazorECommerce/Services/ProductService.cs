using BlazorECommerce.Dtos;

namespace BlazorECommerce.Services;


public class ProductService : IProductService
{
    private readonly List<ProductDto> _products;

    public ProductService()
    {
        _products = SampleProductData.GetAll();
    }
    public Task<List<ProductDto>> GetAllProductsAsync()
    {
        return Task.FromResult(_products);
    }

    public Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }
        return Task.FromResult(product);
    }
}
