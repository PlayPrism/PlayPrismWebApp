namespace PlayPrism.Core.Domain;

public class PaymentMethod : BaseEntity
{
    public string Name { get; set; }
    
    public IEnumerable<Order> Orders { get; set; }
}