namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table ProductCategory in database.
/// </summary>
public class ProductCategory : BaseEntity
{
    /// <summary>
    /// Gets or sets product category name.
    /// </summary>
    public string CategoryName { get; set; }
    
    /// <summary>
    /// Gets or sets product configurations.
    /// </summary>
    public IList<ProductConfiguration> ProductConfigurations { get; set; }
}