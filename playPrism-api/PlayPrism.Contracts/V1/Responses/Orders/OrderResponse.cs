using PlayPrism.Contracts.V1.Responses.UserProfiles;
using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.V1.Responses.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public UserProfileResponse UserProfile { get; set; }

        public Guid PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal OrderTotal { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
