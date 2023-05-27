using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.Extensions;
using PlayPrism.Contracts.V1.Requests.Auth;
using PlayPrism.Contracts.V1.Responses.Auth;

namespace PlayPrism.API.Controllers.V1;

/// <inheritdoc />
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountController> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AccountController" /> class.
    /// </summary>
    /// <param name="accountService">The account service.</param>
    /// <param name="logger">The Logger.</param>
    public AccountController(IAccountService accountService, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(AuthRequest request, CancellationToken cancellationToken)
    {
        var responseDto =
            await _accountService.LoginAsync(request.Email, request.Password, cancellationToken);

        //var cookie = new Cookie(Cook)

        //Response.Cookies["authCookie"]

        if (responseDto == null)
        {
            return Unauthorized("Failed to login".ToErrorResponse());
        }

        var response = new AuthResponse
        {
            Role = responseDto.Role,
            UserId = responseDto.UserId,
            Email = responseDto.Email,
            AccessToken = responseDto.AccessToken
        };
        
        return Ok(response.ToApiResponse());
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(AuthRequest request, CancellationToken cancellationToken)
    {
        var registrationResponse =
            await _accountService.RegisterAsync(request.Email, request.Password,
                cancellationToken);

        if (registrationResponse == null)
        {
            return Unauthorized($"User with such email {request.Email} already exists.".ToErrorResponse());
        }

        var response = new AuthResponse
        {
            Role = registrationResponse.Role,
            UserId = registrationResponse.UserId,
            Email = registrationResponse.Email,
            AccessToken = registrationResponse.AccessToken,
        };

        return Ok(response.ToApiResponse());
    }

    //
    // [HttpGet]
    // [Route("logout")]
    // public Task<IActionResult> Logout(CancellationToken cancellationToken)
    // {
    //     
    // }
}