using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interfaces;
using PlayPrism.Contracts.Extensions;
using PlayPrism.Contracts.V1.Requests.Giveaways;
using PlayPrism.Contracts.V1.Requests.Users;
using PlayPrism.Contracts.V1.Responses.Orders;
using PlayPrism.Contracts.V1.Responses.UserProfiles;
using PlayPrism.Core.Domain;

namespace PlayPrism.API.Controllers.V1
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
                       IUserService usersService, ILogger<UsersController> logger)
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var user = await _usersService
                .GetUserByIdAsync(id, cancellationToken);
            if (user is null)
            {
                _logger.LogError($"User with {id} id not found");
                return NotFound($"User with {id} id not found".ToApiResponse());
            }

            return Ok(user.ToApiResponse());
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersAsync([FromQuery] GetUsersRequest request, CancellationToken cancellationToken) 
        {
            var users = await _usersService
                    .GetUsersAsync(request.PageInfo, cancellationToken);
            if (users is null)
            {
                _logger.LogError($"Users not found");
                return NotFound($"Users not found".ToErrorResponse());
            }

            return Ok(users.ToApiResponse());
        }
    }
}
