using PlayPrism.Core.Enums;

namespace PlayPrism.Contracts.V1.Responses.Auth;

public class LoginResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public Role Role { get; set; }
    public string Token { get; set; }
}