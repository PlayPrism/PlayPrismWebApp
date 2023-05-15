using PlayPrism.Core.Domain;

namespace PlayPrism.Contracts.V1.Requests.Orders;

/// <summary>
///     Create order request
/// </summary>
public class AddOrderRequest
{
    /// <summary>
    ///     Gets or sets order details
    /// </summary>
    public Guid PaymentMethodId { get; set; }

    /// <summary>
    ///     Gets or sets property of navigation property for order itmes
    /// </summary>
    public virtual IEnumerable<OrderItemRequest> OrderItems { get; set; }
}