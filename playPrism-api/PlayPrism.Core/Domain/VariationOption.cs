namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table VariationOption in database.
/// </summary>
public class VariationOption : BaseEntity
{
    /// <summary>
    /// Gets or sets product id
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets products
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Gets or sets variation Value.
    /// </summary>
    public string Value { get; set; }


    /// <summary>
    /// Gets or sets product configuration id
    /// </summary>
    public Guid ProductConfigurationId { get; set; }

    /// <summary>
    /// Gets or sets references to ProductConfigurations entities.
    /// </summary>
    public ProductConfiguration ProductConfiguration { get; set; }
}