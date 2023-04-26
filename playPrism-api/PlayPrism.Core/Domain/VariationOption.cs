namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table VariationOption in database.
/// </summary>
public class VariationOption : BaseEntity
{
    /// <summary>
    /// Gets or sets variation Values.
    /// </summary>
    public string[] Values { get; set; }

    /// <summary>
    /// Gets or sets references to ProductConfigurations entities.
    /// </summary>
    public IList<ProductConfiguration> ProductConfigurations { get; set; }
}