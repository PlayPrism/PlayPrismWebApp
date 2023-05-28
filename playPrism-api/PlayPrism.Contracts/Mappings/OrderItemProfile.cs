using AutoMapper;
using PlayPrism.Contracts.V1.Responses.Orders;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.Mappings
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile() 
        {
            CreateMap<OrderItem, HistoryItemResponse>()
                .ForMember(p => p.ProductId, opt => opt.MapFrom(m => m.ProductItem.ProductId))
                .ForMember(p => p.UserId, opt => opt.MapFrom(m => m.Order.UserId))
                .ForMember(p => p.Name, opt => opt.MapFrom(m => m.ProductItem.Product.Name))
                .ForMember(p => p.Rating, opt => opt.MapFrom(m => 5))
                .ForMember(p => p.Price, opt => opt.MapFrom(m => m.ProductItem.Product.Price))
                .ForMember(p => p.HeaderImage, opt => opt.MapFrom(m => m.ProductItem.Product.HeaderImage))
                .ForMember(p => p.PurchaseDate, opt => opt.MapFrom(m => m.DateCreated))
                .ForMember(p => p.Value, opt => opt.MapFrom(m => m.ProductItem.Value));
        }
        
    }
}
