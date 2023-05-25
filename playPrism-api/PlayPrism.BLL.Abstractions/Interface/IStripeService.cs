using PlayPrism.Contracts.V1.Requests.Stripes;
using PlayPrism.Core.External.Stripe;

namespace PlayPrism.BLL.Abstractions.Interface
{
    /// <summary>
    /// Represents Stripe setive
    /// </summary>
    public interface IStripeService
    {
        /// <summary>
        /// Adds a new Stripe customer asynchronously.
        /// </summary>
        /// <param name="customer"><see cref="AddStripeCustomer"/></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The added Stripe customer.</returns>
        Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new Stripe payment asynchronously.
        /// </summary>
        /// <param name="payment"><see cref="AddStripePayment"/></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The added Stripe payment.</returns>
        Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken cancellationToken);
    }
}