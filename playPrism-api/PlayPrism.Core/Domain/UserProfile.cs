using PlayPrism.Core.Enums;

namespace PlayPrism.Core.Domain;

public class UserProfile : BaseEntity
{
    public string Nickname { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    public string Password { get; set; }
    
    public string Image { get; set; }
    
    public Role Role { get; set; } 
    
    public IEnumerable<Order> Orders { get; set; }
}