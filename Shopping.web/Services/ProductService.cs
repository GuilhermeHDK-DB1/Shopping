using Shopping.web.Models;
using Shopping.web.Services.IServices;
using Shopping.web.Utils;

namespace Shopping.web.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    public const string BasePath = "api/v1/product";

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductModel>> FindAllAsync()
    {
        var response = await _httpClient.GetAsync(BasePath);
        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> FindByIdAsync(long id)
    {
        var response = await _httpClient.GetAsync($"{BasePath}/{id}");
        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> CreateAsync(ProductModel product)
    {
        var response = await _httpClient.PostAsJson($"{BasePath}", product);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong creating the product");
        
        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> UpdateAsync(ProductModel product)
    {
        var response = await _httpClient.PutAsJson($"{BasePath}", product);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong creating the product");
        
        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong creating the product");
        
        return await response.ReadContentAs<bool>();
    }
}