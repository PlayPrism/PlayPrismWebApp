using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.V1.Requests.Stripes;
using PlayPrism.Core.External.Stripe ;
using StripeServices = Stripe;

namespace PlayPrism.BLL.Services;

/// <inheritdoc />
public class StripeService: IStripeService
{
    private readonly StripeServices.ChargeService _chargeService;
    private readonly StripeServices.CustomerService _customerService;
    private readonly StripeServices.TokenService _tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsService"/> class.
    /// </summary>
    /// <param name="chargeService"><see cref="StripeServices.ChargeService"/></param>
    /// <param name="customerService"><see cref="StripeServices.CustomerService"/></param>
    /// <param name="tokenService"><see cref="StripeServices.TokenService"/></param>
    public StripeService(
        StripeServices.ChargeService chargeService,
        StripeServices.CustomerService customerService,
        StripeServices.TokenService tokenService)
    {
        _chargeService = chargeService;
        _customerService = customerService;
        _tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer, CancellationToken cancellationToken)
    {
        // Set Stripe Token options based on customer data
        var tokenOptions = new StripeServices.TokenCreateOptions
        {
            Card = new StripeServices.TokenCardOptions
            {
                Name = customer.Name,
                Number = customer.CreditCard.CardNumber,
                ExpYear = customer.CreditCard.ExpirationYear,
                ExpMonth = customer.CreditCard.ExpirationMonth,
                Cvc = customer.CreditCard.Cvc
            }
        };

        // Create new Stripe Token
        var stripeToken = await _tokenService.CreateAsync(tokenOptions, null, cancellationToken);

        // Set Customer options using
        var customerOptions = new StripeServices.CustomerCreateOptions
        {
            Name = customer.Name,
            Email = customer.Email,
            Source = stripeToken.Id
        };

        // Create customer at Stripe
        var createdCustomer = await _customerService.CreateAsync(customerOptions, null, cancellationToken);

        // Return the created customer at stripe
        var stripeCustomer = new StripeCustomer
        {
            CustomerId = createdCustomer.Id,
            Name = createdCustomer.Name,
            Email = createdCustomer.Email,
        };
        return stripeCustomer;
    }

    /// <inheritdoc />
    public async Task <StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken cancellationToken) 
    {
        // Set the options for the payment we would like to create at Stripe
        var paymentOptions = new StripeServices.ChargeCreateOptions {
            Customer = payment.CustomerId,
            ReceiptEmail = payment.ReceiptEmail,
            Description = payment.Description,
            Currency = payment.Currency,
            Amount = payment.Amount
        };

        // Create the payment
        var createdPayment = await _chargeService.CreateAsync(paymentOptions, null, cancellationToken);

        // Return the payment to requesting method
        var stripePayment = new StripePayment()
        {
            CustomerId = createdPayment.CustomerId,
            ReceiptEmail = createdPayment.ReceiptEmail,
            Description = createdPayment.Description,
            Currency = createdPayment.Currency,
            Amount = createdPayment.Amount,
            PaymentId = createdPayment.Id
        };
        
        return stripePayment;
    }
}