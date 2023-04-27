namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table OrderItem in database.
/// </summary>
public class OrderItem : BaseEntity
{
    /// <summary>
    /// Gets or sets FK id to Order entity.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Gets or sets reference to Order entity.
    /// </summary>
    public Order Order { get; set; }

    /// <summary>
    /// Gets or sets FK id to ProductItem entity.
    /// </summary>
    public Guid ProductItemId { get; set; }

    /// <summary>
    /// Gets or sets reference to ProductItem entity.
    /// </summary>
    public ProductItem ProductItem { get; set; }

    /// <summary>
    /// Gets or sets Quantity of OrderItems entity.
    /// </summary>
    public int Quantity { get; set; }
}