using AutoMapper;
using Shopping.CouponAPI.Data.ValueObjects;
using Shopping.CouponAPI.Model;

namespace Shopping.CouponAPI.Config;

public class MappingConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        var mappingConfig = new MapperConfiguration(
            config =>
            {
                config.CreateMap<CouponVo, Coupon>();
                config.CreateMap<Coupon, CouponVo>();
            });
        return mappingConfig;
    }
}