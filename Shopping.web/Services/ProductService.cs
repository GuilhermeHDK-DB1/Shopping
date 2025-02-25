using System.Net.Http.Headers;
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

    public async Task<IEnumerable<ProductViewModel>> FindAllAsync()
    {
        var response = await _httpClient.GetAsync(BasePath);
        return await response.ReadContentAs<List<ProductViewModel>>();
    }

    public async Task<ProductViewModel> FindByIdAsync(long id, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.GetAsync($"{BasePath}/{id}");
        return await response.ReadContentAs<ProductViewModel>();
    }

    public async Task<ProductViewModel> CreateAsync(ProductViewModel productView, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.PostAsJson($"{BasePath}", productView);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong creating the product");
        
        return await response.ReadContentAs<ProductViewModel>();
    }

    public async Task<ProductViewModel> UpdateAsync(ProductViewModel productView, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.PutAsJson($"{BasePath}", productView);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong updating the product");
        
        return await response.ReadContentAs<ProductViewModel>();
    }

    public async Task<bool> DeleteAsync(long id, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong deleting the product");
        
        return await response.ReadContentAs<bool>();
    }
}