namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table VariationOption in database.
/// </summary>
public class VariationOption : BaseEntity
{
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    /// <summary>
    /// Gets or sets variation Value.
    /// </summary>
    public string Value { get; set; }


    public Guid ProductConfigurationId { get; set; }

    /// <summary>
    /// Gets or sets references to ProductConfigurations entities.
    /// </summary>
    public ProductConfiguration ProductConfiguration { get; set; }
}