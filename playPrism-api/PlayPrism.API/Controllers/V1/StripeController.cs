using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.V1.Requests.Stripes;
using PlayPrism.Core.External.Stripe;

namespace PlayPrism.API.Controllers.V1;

/// <inheritdoc />
[Route("api/[controller]")]
public class StripeController : ControllerBase
{
    private readonly IStripeService _stripeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="StripeController"/> class.
    /// </summary>
    /// <param name="stripeService"><see cref="IStripeService"/></param>
    public StripeController(IStripeService stripeService)
    {
        _stripeService = stripeService;
    }

    /// <summary>
    /// Adds a new Stripe customer.
    /// </summary>
    /// <param name="customer"><see cref="AddStripeCustomer"/></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><see cref="StripeCustomer"/></returns>
    [HttpPut("customer")]
    public async Task<ActionResult<StripeCustomer>> AddStripeCustomer(
        [FromBody] AddStripeCustomer customer,
        CancellationToken cancellationToken)
    {
        var createdCustomer = await _stripeService.AddStripeCustomerAsync(
            customer,
            cancellationToken);

        return Ok(createdCustomer);
    }

    /// <summary>
    /// Adds a new Stripe payment.
    /// </summary>
    /// <param name="payment"><see cref="AddStripePayment"/></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><see cref="StripePayment"/></returns>
    [HttpPut("payment")]
    public async Task<ActionResult<StripePayment>> AddStripePayment(
        [FromBody] AddStripePayment payment,
        CancellationToken cancellationToken)
    {
        var createdPayment = await _stripeService.AddStripePaymentAsync(
            payment,
            cancellationToken);

        return Ok(createdPayment);
    }
}