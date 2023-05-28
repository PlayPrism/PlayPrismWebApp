using PlayPrism.Contracts.V1.Responses.Products;
using PlayPrism.Contracts.V1.Responses.UserProfiles;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.V1.Responses.Giveaways
{
    public class GiveawayResponse
    {
        public Guid Id { get; set; }

        public ProductResponse Product { get; set; }

        public IList<UserProfileResponse> Participants { get; set; }

        public UserProfileResponse Winner { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
