using PlayPrism.Contracts.V1.Responses.Products;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.V1.Responses.Giveaways
{
    public class GiveawayResponse
    {
        public Guid Id { get; set; }

        public ProductResponse Product { get; set; }

        public IList<UserProfile> Participants { get; set; }

        public Guid WinnerId { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
