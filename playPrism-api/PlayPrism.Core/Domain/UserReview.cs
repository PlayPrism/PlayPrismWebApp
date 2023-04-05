namespace PlayPrism.Core.Domain;

public class UserReview : BaseEntity
{
    public Guid UserId { get; set; }
    
    public UserProfile User { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public int RatingValue { get; set; }
    
    public string Comment { get; set; }
}