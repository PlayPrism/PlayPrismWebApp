using AutoMapper;
using PlayPrism.BLL.Abstractions.Interfaces;
using PlayPrism.BLL.Constants;
using PlayPrism.Contracts.V1.Responses.Orders;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<HistoryItemResponse>> GetUserHistoryAsync(Guid id, CancellationToken cancellationToken) 
        {
            var orderItems = await _unitOfWork.OrderItems
                .GetByConditionAsync(
                orderItem => orderItem.Order.UserId == id,
                EntitiesSelectors.HistoryItemSelector,
                cancellationToken);
            var result = _mapper.Map<List<HistoryItemResponse>>(orderItems);
            return result;
        }
    }
}
