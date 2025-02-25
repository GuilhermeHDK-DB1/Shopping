using Shopping.web.Models;

namespace Shopping.web.Services.IServices;

public interface ICartService
{
    Task<CartViewModel> FindCartByUserIdAsync(string userId, string token);
    Task<CartViewModel> AddItemToCartAsync(CartViewModel cart, string token);
    Task<CartViewModel> UpdateCartAsync(CartViewModel cart, string token);
    Task<bool> RemoveCartAsync(long cartId, string token);
    
    Task<bool> ApplyCouponAsync(CartViewModel cart, string couponCode, string token);
    Task<bool> RemoveCouponAsync(string userId, string token);
    Task<bool> ClearCart(string userId, string token);
    
    Task<CartViewModel> CheckoutAsync(CartHeaderViewModel cartHeader, string token);
}