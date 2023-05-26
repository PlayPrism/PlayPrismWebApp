using PlayPrism.Contracts.V1.Responses.Giveaways;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Domain.Filters;

namespace PlayPrism.BLL.Abstractions.Interface
{
    public interface IGiveawaysService
    {
        Task<IList<GiveawayResponse>> GetGiveawaysAsync(PageInfo pageInfo, CancellationToken cancellationToken);

        Task<GiveawayResponse> GetGiveawayByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
