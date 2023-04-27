namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table PaymentMethod in database.
/// </summary>
public class PaymentMethod : BaseEntity
{
    /// <summary>
    /// Gets or sets paymentMethod Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets references to Orders entities.
    /// </summary>
    public IList<Order> Orders { get; set; }
}