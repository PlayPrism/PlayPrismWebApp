using AutoMapper;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.BLL.Constants;
using PlayPrism.Contracts.V1.Responses.Giveaways;
using PlayPrism.Contracts.V1.Responses.ProductItems;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Domain.Filters;
using PlayPrism.DAL.Abstractions.Interfaces;
using System.Linq.Expressions;

namespace PlayPrism.BLL.Services
{
    public class GiveawayService : IGiveawaysService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GiveawayService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GiveawayResponse>> GetGiveawaysAsync(
            PageInfo pageInfo,
            CancellationToken cancellationToken)
        {
            try
            {
                var giveaways = await _unitOfWork.Giveaways
                    .GetPageWithMultiplePredicatesAsync(null, pageInfo, EntitiesSelectors.GiveawaySelector, cancellationToken);
                var result = _mapper.Map<List<GiveawayResponse>>(giveaways);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<GiveawayResponse> GetGiveawayByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var giveaway = (await _unitOfWork.Giveaways
               .GetByConditionAsync(
                    giveaway => giveaway.Id == id,
                    EntitiesSelectors.GiveawaySelector,
                    cancellationToken)).FirstOrDefault();

            var result = _mapper.Map<GiveawayResponse>(giveaway);
            return result;
        }

        public async Task<ProductItemResponse> GetPrizeAsync(Guid id, CancellationToken cancellationToken)
        {
            var giveaways = await _unitOfWork.Giveaways
               .GetByConditionAsync(
                    giveaway => giveaway.Id == id,
                    EntitiesSelectors.GiveawaySelector,
                    cancellationToken);

            var res = giveaways.FirstOrDefault();

            //res.Product.ProductItems.FirstOrDefault();

            //var productItem = res.Product.ProductItems.FirstOrDefault();

            var result = _mapper.Map<ProductItemResponse>(res);
            return result;
        }
    }
}
