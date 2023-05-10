using AutoMapper;
using PlayPrism.Contracts.V1.Responses.Products;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductConfiguration, CategoryFiltersResponse>()
            .ForMember(response => response.FilterOptions,
            opt => opt
                .MapFrom(product =>
                    product.VariationOptions.Select(option => option.Value).Distinct().ToArray()));

        CreateMap<Product, ProductResponse>()
            .ForMember(response => response.Genres,
                opt => opt
                    .MapFrom(product =>
                        product.VariationOptions.Where(option =>
                                option.ProductConfiguration.ConfigurationName == "Genre")
                            .Select(option => option.Value)))
            .ForMember(dest => dest.Platforms,
                opt => opt
                    .MapFrom(
                        product => product.VariationOptions.Where(option =>
                                option.ProductConfiguration.ConfigurationName == "Platform")
                            .Select(option => option.Value)
                    ));
    }
}