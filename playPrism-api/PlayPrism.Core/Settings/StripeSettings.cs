namespace PlayPrism.Core.Settings;

/// <summary>
/// Represents stripe settings
/// </summary>
public class StripeSettings
{
    /// <summary>
    /// Gets or sets key that is used by the client application.
    /// </summary>
    public string PublishedKey { get; set; }
    
    /// <summary>
    /// Gets or sets key that is used in the authentication process at the backend server.
    /// </summary>
    public string SecretKey { get; set; }
}