using Shopping.web.Models;

namespace Shopping.web.Services.IServices;

public interface ICouponService
{
    Task<CouponViewModel> GetCouponAsync(string couponCode, string token);
}