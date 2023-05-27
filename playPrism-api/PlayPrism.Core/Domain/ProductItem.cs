namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table ProductItem in database.
/// </summary>
public class ProductItem : BaseEntity
{
    /// <summary>
    /// Gets or sets FK id to Product entity.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets reference to Product entity.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Gets or sets product value that can be key, code or account credentials.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid? OrderItemId { get; set; }

    /// <summary>
    /// Gets or sets reference to OrderItem entity.
    /// </summary>
    public OrderItem? OrderItem { get; set; }
}