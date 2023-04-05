namespace PlayPrism.Core.Domain;

public class ProductConfiguration : BaseEntity
{
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public Guid VariationOptionId { get; set; }
    
    public VariationOption VariationOption { get; set; }
}