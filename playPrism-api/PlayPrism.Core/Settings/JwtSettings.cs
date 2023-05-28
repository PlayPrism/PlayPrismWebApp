namespace PlayPrism.Core.Settings;

public class JwtSettings
{
    public string Key { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }

    public TimeSpan AccessTokenLifetime { get; set; }
    
    public TimeSpan RefreshTokenLifeTime { get; set; }

}