namespace PlayPrism.Core.Domain;

/// <inheritdoc />
public class ResetPasswordCode : BaseEntity
{
    public Guid ProfileUserId { get; set; }
    public UserProfile User { get; set; }
    public string Code { get; set; }
    public DateTime ExpirationDate { get; set; }
}