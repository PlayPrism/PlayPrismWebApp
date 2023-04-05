namespace PlayPrism.Core.Domain;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
    
    public Guid ProductItemId { get; set; }
    
    public ProductItem ProductItem { get; set; }
    
    public int Quantity { get; set; }
}