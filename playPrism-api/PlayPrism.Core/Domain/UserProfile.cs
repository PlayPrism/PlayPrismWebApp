using PlayPrism.Core.Enums;

namespace PlayPrism.Core.Domain;

/// <summary>
/// Entity that represents table UserProfile in database.
/// </summary>
public class UserProfile : BaseEntity
{
    /// <summary>
    /// Gets or sets Nickname.
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// Gets or sets Email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets Phone.
    /// </summary>
    public string Phone { get; set; }


    /// <summary>
    /// Gets or sets Password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets Image url.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// Gets or sets reference to Role entity.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Gets or sets references to Orders entities.
    /// </summary>
    public IList<Order> Orders { get; set; }

    /// <summary>
    /// Gets or sets reference to Refresh Token entity.
    /// </summary>
    public RefreshToken RefreshToken { get; set; }
    
    /// <summary>
    /// Gets or sets reference to Refresh Token entity.
    /// </summary>
    public ResetPasswordCode ResetPasswordCode { get; set; }

    /// <summary>
    /// Gets or sets references to Giveaways entities.
    /// </summary>
    public IList<Giveaway> Giveaways { get; set; }


    /// <summary>
    /// Gets or sets references to Giveaways entities in which the user is a winner.
    /// </summary>
    public IList<Giveaway> WonGiveaways { get; set; }
}