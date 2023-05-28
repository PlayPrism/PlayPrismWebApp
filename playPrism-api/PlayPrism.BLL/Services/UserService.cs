using AutoMapper;
using PlayPrism.BLL.Abstractions.Interfaces;
using PlayPrism.BLL.Constants;
using PlayPrism.Contracts.V1.Responses.Giveaways;
using PlayPrism.Contracts.V1.Responses.Orders;
using PlayPrism.Contracts.V1.Responses.UserProfiles;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Domain.Filters;
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

        public async Task<UserProfileResponse> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = (await _unitOfWork.Users
                .GetByConditionAsync(
                user => user.Id == id,
                EntitiesSelectors.UserSelector,
                cancellationToken)).FirstOrDefault();
            var result = _mapper.Map<UserProfileResponse>(user);
            return result;
        }

        public async Task<IList<UserProfileResponse>> GetUsersAsync(PageInfo pageInfo, CancellationToken cancellationToken) 
        {
            try
            {
                var users = await _unitOfWork.Users
                    .GetPageWithMultiplePredicatesAsync(null, pageInfo, EntitiesSelectors.UserSelector, cancellationToken);
                var result = _mapper.Map<List<UserProfileResponse>>(users);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
