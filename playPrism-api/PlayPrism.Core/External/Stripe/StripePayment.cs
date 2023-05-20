namespace PlayPrism.Core.External.Stripe;

/// <summary>
/// Represents a Stripe payment.
/// </summary>
public class StripePayment
{
    /// <summary>
    /// Gets or sets the customer ID.
    /// </summary>
    public string CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the receipt email.
    /// </summary>
    public string ReceiptEmail { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Gets or sets the amount.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// Gets or sets the payment ID.
    /// </summary>
    public string PaymentId { get; set; }
}