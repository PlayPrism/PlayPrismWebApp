using PlayPrism.Contracts.V1.Responses.Giveaways;
using PlayPrism.Contracts.V1.Responses.ProductItems;
using PlayPrism.Core.Domain.Filters;

namespace PlayPrism.BLL.Abstractions.Interface
{
    public interface IGiveawaysService
    {
        /// <summary>
        /// Asynchronously returns a list of giveaways.
        /// </summary>
        /// <param name="pageInfo">The pagination parameter.</param>
        /// <param name="cancellationToken">The cancellation taken.</param>
        /// <returns></returns>
        Task<IList<GiveawayResponse>> GetGiveawaysAsync(PageInfo pageInfo, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously returns a giveaway by id.
        /// </summary>
        /// <param name="id">The giveaway id.</param>
        /// <param name="cancellationToken">The cancellation taken.</param>
        /// <returns></returns>
        Task<GiveawayResponse> GetGiveawayByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<ProductItemResponse> GetPrizeAsync(Guid id, CancellationToken cancellationToken);
    }
}
