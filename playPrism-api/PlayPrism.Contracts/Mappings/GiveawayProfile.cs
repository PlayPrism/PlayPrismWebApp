using AutoMapper;
using PlayPrism.Contracts.V1.Responses.Giveaways;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.Mappings
{
    public class GiveawayProfile : Profile
    {
        public GiveawayProfile() 
        {
            CreateMap<Giveaway, GiveawayResponse>();
                
        }
    }
}
