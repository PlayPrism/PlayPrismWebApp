namespace PlayPrism.Contracts.V1.Requests.Stripes;

/// <summary>
/// Represents the card details to be added.
/// </summary>
public class AddStripeCard
{
    /// <summary>
    /// Gets or sets the cardholder's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the card number.
    /// </summary>
    public string CardNumber { get; set; }

    /// <summary>
    /// Gets or sets the expiration year.
    /// </summary>
    public string ExpirationYear { get; set; }

    /// <summary>
    /// Gets or sets the expiration month.
    /// </summary>
    public string ExpirationMonth { get; set; }

    /// <summary>
    /// Gets or sets the CVC code.
    /// </summary>
    public string Cvc { get; set; }
}