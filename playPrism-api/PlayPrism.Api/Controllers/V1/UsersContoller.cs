using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interfaces;
using PlayPrism.Contracts.Extensions;
using PlayPrism.Contracts.V1.Responses.Orders;

namespace PlayPrism.API.Controllers.V1
{
    public class UsersContoller : ControllerBase
    {
        private readonly IUserService _usersService;
        private readonly ILogger<UsersContoller> _logger;

        public UsersContoller(
                       IUserService usersService, ILogger<UsersContoller> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [HttpGet("{id}/history")]
        [ProducesResponseType(typeof(HistoryItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserHistoryAsync([FromRoute] Guid id, CancellationToken cancellationToken) 
        {
            var historyItems = await _usersService
                .GetUserHistoryAsync(id, cancellationToken);
            if (historyItems is null)
            {
                _logger.LogError($"User with {id} id not found");
                return NotFound($"User with {id} id not found".ToApiResponse());
            }
            return Ok(historyItems.ToApiResponse());
        }
    }
}
