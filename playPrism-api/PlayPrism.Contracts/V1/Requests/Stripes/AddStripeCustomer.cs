namespace PlayPrism.Contracts.V1.Requests.Stripes;

/// <summary>
/// Represents the customer details to be added
/// </summary>
public class AddStripeCustomer
{
    /// <summary>
    /// Gets or sets the customer email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the customer name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the credit card details.
    /// </summary>
    public AddStripeCard CreditCard { get; set; }
}