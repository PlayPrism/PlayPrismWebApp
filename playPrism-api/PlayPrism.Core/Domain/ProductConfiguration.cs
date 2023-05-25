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
    /// Gets or sets FK id to ProductCategory entity.
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Gets or sets reference to Category entity.
    /// </summary>
    public ProductCategory Category { get; set; }
    
    /// <summary>
    /// Gets or sets reference to Product entity.
    /// </summary>
    public List<VariationOption> VariationOptions { get; set; }
}