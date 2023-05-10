using AutoMapper;
using PlayPrism.Contracts.V1.Responses.Products;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.Mappings;

public class CatalogueProfile : Profile
{
    public CatalogueProfile()
    {
        // CreateMap<Product, GetProductsResponse>()
        //     .ForMember(dest => dest.Genres,
        //         opt => opt
        //             .MapFrom(product =>
        //                 product.ProductConfigurations
        //                     .FirstOrDefault(pc => pc.ConfigurationName == "Genre")!
        //                     .VariationOption.Values.ToList()
        //             ))
        //     .ForMember(dest => dest.Platforms,
        //         opt => opt.MapFrom(product =>
        //             product.ProductConfigurations
        //                 .FirstOrDefault(configuration => configuration.ConfigurationName == "Platforms")!
        //                 .VariationOption.Values.ToList()))
        //     .ForMember(dest => dest.Title,
        //         opt => opt.MapFrom(product => product.Name));

        CreateMap<Product, GetProductsResponse>()
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
                    ))
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(product => product.Name));
    }
}