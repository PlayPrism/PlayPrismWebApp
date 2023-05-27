using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.Extensions;
using PlayPrism.Contracts.V1.Requests.Giveaways;
using PlayPrism.Contracts.V1.Responses.Giveaways;
using PlayPrism.Contracts.V1.Responses.ProductItems;

namespace PlayPrism.API.Controllers.V1
{
    [Route("api/[controller]")]
    public class GiveawaysController : ControllerBase
    {
        private readonly IGiveawaysService _giveawaysService;
        private readonly ILogger<GiveawaysController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GiveawaysController"/> class.
        /// </summary>
        /// <param name="giveawaysService"><see cref="IGiveawaysService"/></param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/></param>
        public GiveawaysController(
            IGiveawaysService giveawaysService, ILogger<GiveawaysController> logger)
        {
            _giveawaysService = giveawaysService;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GiveawayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGiveawayByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var giveaway = await _giveawaysService
                .GetGiveawayByIdAsync(id, cancellationToken);

            if (giveaway is null)
            {
                _logger.LogError($"Giveaway with {id} id not found");
                return NotFound($"Giveaway with {id} id not found".ToErrorResponse());
            }

            return Ok(giveaway.ToApiResponse());
        }


        [HttpGet]
        [ProducesResponseType(typeof(GiveawayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGiveaways([FromQuery] GetGiveawaysRequest request, CancellationToken cancellationToken)
        {
            var giveaways = await _giveawaysService
                    .GetGiveawaysAsync(request.PageInfo, cancellationToken);
            if (giveaways is null)
            {
                _logger.LogError($"Giveaways not found");
                return NotFound($"Giveaways not found".ToErrorResponse());
            }

            return Ok(giveaways.ToApiResponse());
        }

        [HttpGet("{id}/prize")]
        [ProducesResponseType(typeof(ProductItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPrizeAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var prize = await _giveawaysService
                .GetPrizeAsync(id, cancellationToken);

            if (prize is null)
            {
                _logger.LogError($"Prize with {id} id not found");
                return NotFound($"Prize with {id} id not found".ToErrorResponse());
            }

            return Ok(prize.ToApiResponse());
        }
    }
}
