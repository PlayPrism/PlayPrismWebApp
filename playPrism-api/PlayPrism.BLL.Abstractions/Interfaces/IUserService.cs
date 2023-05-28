using PlayPrism.Contracts.V1.Responses.Orders;
using PlayPrism.Contracts.V1.Responses.UserProfiles;
using PlayPrism.Core.Domain.Filters;

namespace PlayPrism.BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        public Task<IList<HistoryItemResponse>> GetUserHistoryAsync(Guid id, CancellationToken cancellationToken);

        public Task<UserProfileResponse> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

        public Task<IList<UserProfileResponse>> GetUsersAsync(PageInfo pageInfo, CancellationToken cancellationToken);
    }
}
