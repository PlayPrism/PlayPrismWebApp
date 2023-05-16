namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table Order in database.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Gets or sets FK id to User entity.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets reference to UserProfile entity.
    /// </summary>
    public UserProfile UserProfile { get; set; }

    /// <summary>
    /// Gets or sets FK id to PaymentMethod entity.
    /// </summary>
    public Guid PaymentMethodId { get; set; }

    /// <summary>
    /// Gets or sets reference to PaymentMethod entity.
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Gets or sets OrderTotal sum.
    /// </summary>
    public decimal OrderTotal { get; set; }

    /// <summary>
    /// Gets or sets references to OrderItems entities.
    /// </summary>
    public IList<OrderItem> OrderItems { get; set; }
}