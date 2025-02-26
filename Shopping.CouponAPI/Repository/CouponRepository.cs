using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping.CouponAPI.Data.ValueObjects;
using Shopping.CouponAPI.Model.Context;
using Shopping.CouponAPI.Repository.Interfaces;

namespace Shopping.CouponAPI.Repository;

public class CouponRepository : ICouponRepository
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public CouponRepository(
        Context context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CouponVo> GetCouponByCodeAsync(string couponCode)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
        return _mapper.Map<CouponVo>(coupon);
    }
}