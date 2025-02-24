using AutoMapper;
using Shopping.CartApi.Model;

namespace Shopping.CartApi.Config;

public class MappingConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        var mappingConfig = new MapperConfiguration(
            config =>
            {
                // config.CreateMap<ProductVO, Product>();
                // config.CreateMap<Product, ProductVO>();
            });
        return mappingConfig;
    }
}