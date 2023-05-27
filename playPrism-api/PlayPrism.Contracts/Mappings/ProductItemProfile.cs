using AutoMapper;
using PlayPrism.Contracts.V1.Responses.ProductItems;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.Mappings
{
    public class ProductItemProfile : Profile
    {
        public ProductItemProfile()
        {
            CreateMap<ProductItem, ProductItemResponse>()
                .ForMember(x => x.Product, opt => opt.MapFrom(m => m.Product))
                /*  .ForMember(x => x.Value, opt => opt.MapFrom(m => m.Product.ProductItems.FirstOrDefault())) */
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Id));
              /*  .ForMember(x => x.OrderItem, opt => opt.MapFrom(m => m.OrderItem)); */
        }
    }
}
