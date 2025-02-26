using Shopping.CartApi.Data.ValueObjects;

namespace Shopping.CartApi.Repository;

public interface ICartRepository
{
    Task<CartVo?> FindCartByUserIdAsync(string userId);
    Task<CartVo> SaveOrUpdateCartAsync(CartVo vo);
    Task<bool> RemoveFromCartAsync(long cartDetailsId);
    Task<bool> ApplyCouponAsync(string userId, string couponCode);
    Task<bool> RemoveCouponAsync(string userId);
    Task<bool> ClearCart(string userId);
}