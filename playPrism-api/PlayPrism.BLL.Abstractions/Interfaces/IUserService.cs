using PlayPrism.Contracts.V1.Responses.Orders;

namespace PlayPrism.BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        public Task<IList<HistoryItemResponse>> GetUserHistoryAsync(Guid id, CancellationToken cancellationToken);
    }
}
