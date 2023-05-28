using AutoMapper;
using PlayPrism.Contracts.V1.Responses.Orders;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderResponse>();
        }
    }
}
