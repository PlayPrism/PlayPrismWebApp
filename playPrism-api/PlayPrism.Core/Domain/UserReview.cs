namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table UserReview in database.
/// </summary>
public class UserReview : BaseEntity
{
    /// <summary>
    /// Gets or sets FK id to UserProfile entity.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets reference to User entity.
    /// </summary>
    public UserProfile User { get; set; }

    /// <summary>
    /// Gets or sets FK id to Product entity.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets reference to Product entity.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Gets or sets RatingValue.
    /// </summary>
    public int RatingValue { get; set; }

    /// <summary>
    /// Gets or sets Comment string.
    /// </summary>
    public string Comment { get; set; }
}