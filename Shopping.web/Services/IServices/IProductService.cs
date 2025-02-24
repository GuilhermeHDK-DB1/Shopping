using Shopping.web.Models;

namespace Shopping.web.Services.IServices;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> FindAllAsync();
    Task<ProductModel> FindByIdAsync(long id, string token);
    Task<ProductModel> CreateAsync(ProductModel product, string token);
    Task<ProductModel> UpdateAsync(ProductModel product, string token);
    Task<bool> DeleteAsync(long id, string token);
}