using PlayPrism.Core.Enums;

namespace PlayPrism.Contracts.V1.Responses.Auth;

public class AuthResponse
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public string AccessToken { get; set; }
}