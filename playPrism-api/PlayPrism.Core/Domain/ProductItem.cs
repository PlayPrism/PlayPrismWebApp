namespace PlayPrism.Core.Domain;

public class ProductItem : BaseEntity
{
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public string Value { get; set; }
    
    public OrderItem OrderItem { get; set; }
}