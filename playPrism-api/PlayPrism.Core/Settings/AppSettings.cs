namespace PlayPrism.Core.Settings;

public class AppSettings
{
    public SmtpSettings SmtpSettings { get; set; }
    public JwtSettings JwtSettings { get; set; }
}

public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public string SenderEmail { get; set; }
}

public class JwtSettings
{
    public string Key { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }

    public TimeSpan AccessTokenLifetime { get; set; }
    
    public TimeSpan RefreshTokenLifeTime { get; set; }
}