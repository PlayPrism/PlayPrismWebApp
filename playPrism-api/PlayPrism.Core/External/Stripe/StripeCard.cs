namespace PlayPrism.Core.External.Stripe;

/// <summary>
/// Represents Stripe card
/// </summary>
public class StripeCard
{
    /// <summary>
    /// Gets or sets the name on the card.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the card number.
    /// </summary>
    public string CardNumber { get; set; }

    /// <summary>
    /// Gets or sets the expiration year of the card.
    /// </summary>
    public string ExpirationYear { get; set; }

    /// <summary>
    /// Gets or sets the expiration month of the card.
    /// </summary>
    public string ExpirationMonth { get; set; }

    /// <summary>
    /// Gets or sets the CVC (Card Verification Code) of the card.
    /// </summary>
    public string Cvc { get; set; }
}