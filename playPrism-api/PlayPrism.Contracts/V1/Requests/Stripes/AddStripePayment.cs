namespace PlayPrism.Contracts.V1.Requests.Stripes;

/// <summary>
/// Represents the payment details to be added.
/// </summary>
public class AddStripePayment
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
    /// Gets or sets the payment description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Gets or sets the payment amount.
    /// </summary>
    public long Amount { get; set; }
}