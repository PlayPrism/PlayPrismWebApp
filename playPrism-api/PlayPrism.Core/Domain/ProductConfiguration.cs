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
    /// Gets or sets reference to Category entity.
    /// </summary>
    public ProductCategory Category { get; set; }

    //public Guid VariationOptionId { get; set; }

    /// <summary>
    /// Gets or sets reference to Product entity.
    /// </summary>
    public List<VariationOption> VariationOptions { get; set; }
}