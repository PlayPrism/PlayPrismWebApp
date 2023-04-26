namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table ProductConfiguration in database.
/// </summary>
public class ProductConfiguration : BaseEntity
{
    /// <summary>
    /// Gets or sets ConfigurationName.
    /// </summary>
    public string ConfigurationName { get; set; }

    /// <summary>
    /// Gets or sets FK id to Product entity.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets reference to Product entity.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Gets or sets FK id to VariationOption entity.
    /// </summary>
    public Guid VariationOptionId { get; set; }

    /// <summary>
    /// Gets or sets reference to Product entity.
    /// </summary>
    public VariationOption VariationOption { get; set; }
}