namespace PlayPrism.Core.Domain;

public class VariationOption : BaseEntity
{
    public string Type { get; set; }
    
    public string[] Values { get; set; }
    
    public Guid ProductConfigurationId { get; set; }
    
    public IEnumerable<ProductConfiguration> ProductConfigurations { get; set; }
}