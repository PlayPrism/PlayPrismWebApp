using PlayPrism.Core.Domain;
using PlayPrism.Core.Enums;

namespace PlayPrism.Contracts.V1.Responses.UserProfiles
{
    public class UserProfileResponse
    {
        public Guid Id { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Image { get; set; }

        public Role Role { get; set; }

        public IList<Order> Orders { get; set; }

        public IList<Giveaway> Giveaways { get; set; }

        public IList<Giveaway> WonGiveaways { get; set; }
    }
}
