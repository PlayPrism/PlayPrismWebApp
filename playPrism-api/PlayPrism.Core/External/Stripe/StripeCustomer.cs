namespace PlayPrism.Core.External.Stripe;

/// <summary>
/// Represents Stripe customer
/// </summary>
public class StripeCustomer
{
    /// <summary>
    /// Gets or sets the customer ID.
    /// </summary>
    public string CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the email of the customer.
    /// </summary>
    public string Email { get; set; }
}