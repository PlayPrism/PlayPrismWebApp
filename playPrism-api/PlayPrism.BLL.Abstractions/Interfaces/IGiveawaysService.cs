using PlayPrism.Contracts.V1.Responses.Giveaways;
using PlayPrism.Contracts.V1.Responses.ProductItems;
using PlayPrism.Core.Domain.Filters;

namespace PlayPrism.BLL.Abstractions.Interface
{
    /// <summary>
    /// Giweaway service that works with giveaways.
    /// </summary>
    public interface IGiveawaysService
    {
        /// <summary>
        /// Asynchronously returns a list of giveaways.
        /// </summary>
        /// <param name="pageInfo">The pagination parameter.</param>
        /// <param name="cancellationToken">The cancellation taken.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation. The task result contains the list of products.</returns>
        Task<IList<GiveawayResponse>> GetGiveawaysAsync(PageInfo pageInfo, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously returns a giveaway by id.
        /// </summary>
        /// <param name="id">The giveaway id.</param>
        /// <param name="cancellationToken">The cancellation taken.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation. The task result contains the list of products.</returns>
        Task<GiveawayResponse> GetGiveawayByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously returns prize of the giweaway by id.
        /// </summary>
        /// <param name="id">The giveaway id.</param>
        /// <param name="cancellationToken">The cancellation taken.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation. The task result contains the list of products.</returns>
        Task<ProductItemResponse> GetPrizeAsync(Guid id, CancellationToken cancellationToken);
    }
}
