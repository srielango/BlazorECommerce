using BlazorECommerce.Dtos;

namespace BlazorECommerce.Services;


public class ProductService : IProductService
{
    private readonly IConfiguration _configuration;

    //private readonly List<ProductDto> _products;
    private readonly ApiClient _apiClient;

    public ProductService(ApiClient apiClient, IConfiguration configuration)
    {
        _apiClient = apiClient;
        _configuration = configuration;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await _apiClient.GetAsync<List<ProductDto>>("catalogapi/products");
        foreach(var product in products)
        {
            product.ImageFile = _configuration["ApiBaseUrl"] + "catalogapi" + product.ImageFile;
        }
        return products;
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _apiClient.GetAsync<ProductDto>($"catalogapi/products/{id}");
        product.ImageFile = _configuration["ApiBaseUrl"] + "catalogapi" + product.ImageFile;
        return product;
    }
}
