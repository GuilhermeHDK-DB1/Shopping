using Shopping.web.Models;

namespace Shopping.web.Services.IServices;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> FindAllAsync();
    Task<ProductViewModel> FindByIdAsync(long id, string token);
    Task<ProductViewModel> CreateAsync(ProductViewModel productView, string token);
    Task<ProductViewModel> UpdateAsync(ProductViewModel productView, string token);
    Task<bool> DeleteAsync(long id, string token);
}