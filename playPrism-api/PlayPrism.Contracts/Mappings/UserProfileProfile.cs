using AutoMapper;
using PlayPrism.Contracts.V1.Responses.UserProfiles;
using PlayPrism.Core.Domain;
namespace PlayPrism.Contracts.Mappings
{
    public class UserProfileProfile : Profile
    {
        public UserProfileProfile()
        {
            CreateMap<UserProfile, UserProfileResponse>();
        }
    }
}
