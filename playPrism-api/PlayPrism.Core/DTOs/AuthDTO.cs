using PlayPrism.Core.Enums;

namespace PlayPrism.Core.DTOs;

public class AuthDTO
{
    public Guid UserId { get; set; }
    
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public Role Role { get; set; }
}