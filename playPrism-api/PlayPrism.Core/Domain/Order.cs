namespace PlayPrism.Core.Domain;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }

    public UserProfile UserProfile { get; set; }
    
    public Guid PaymentMethodId { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
    
    public decimal OrderTotal { get; set; }
    
    public IEnumerable<OrderItem> OrderItems { get; set; }
    
    public ProductItem ProductItem { get; set; }
}