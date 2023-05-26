namespace PlayPrism.Core.Domain;

public class Giveaway : BaseEntity
{
    public Product Product { get; set; }

    public Guid ProductId { get; set; }

    public IList<UserProfile>? Participants { get; set; }

    public UserProfile? Winner { get; set; }

    public Guid? WinnerId { get; set; }

    public DateTime ExpirationDate { get; set; }
}