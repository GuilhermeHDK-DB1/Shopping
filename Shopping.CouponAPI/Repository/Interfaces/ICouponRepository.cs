using Shopping.CouponAPI.Data.ValueObjects;

namespace Shopping.CouponAPI.Repository.Interfaces;

public interface ICouponRepository
{
    Task<CouponVo> GetCouponByCodeAsync(string couponCode);
}