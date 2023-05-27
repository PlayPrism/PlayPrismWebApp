namespace PlayPrism.Core.Domain;

public class RefreshToken : BaseEntity
{
    public UserProfile User { get; set; }
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
}