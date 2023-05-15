namespace PlayPrism.Contracts.V1.Requests.Orders;

/// <summary>
///     Create order item request
/// </summary>
public class OrderItemRequest
{
    /// <summary>
    ///     Gets or sets good id
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    ///     Gets or sets quantity
    /// </summary>
    public int Quantity { get; set; }
}