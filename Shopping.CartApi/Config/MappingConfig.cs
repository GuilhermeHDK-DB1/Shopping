using AutoMapper;
using Shopping.CartApi.Data.ValueObjects;
using Shopping.CartApi.Model;

namespace Shopping.CartApi.Config;

public class MappingConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        var mappingConfig = new MapperConfiguration(
            config =>
            {
                config.CreateMap<ProductVo, Product>().ReverseMap();
                config.CreateMap<CartHeaderVo, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailVo, CartDetail>().ReverseMap();
                config.CreateMap<CartVo, Cart>().ReverseMap();
            });
        return mappingConfig;
    }
}