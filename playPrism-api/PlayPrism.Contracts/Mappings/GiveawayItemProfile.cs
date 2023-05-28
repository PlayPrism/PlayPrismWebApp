﻿using AutoMapper;
using PlayPrism.Contracts.V1.Responses.ProductItems;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.Mappings
{
    public class GiveawayItemProfile : Profile
    {
        public GiveawayItemProfile()
        {
            CreateMap<Giveaway, ProductItemResponse>()
                .ForMember(x => x.Product, opt => opt.MapFrom(m => m.Product))
                .ForMember(x => x.Value, opt => opt.MapFrom(m => m.Product.ProductItems.FirstOrDefault().Value))
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Product.ProductItems.FirstOrDefault().Id))
                .ForMember(x => x.OrderItem, opt => opt.MapFrom(m => m.Product.ProductItems.FirstOrDefault().OrderItem));
        }
    }
}
