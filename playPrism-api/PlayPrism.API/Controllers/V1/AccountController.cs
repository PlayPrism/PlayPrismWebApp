using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.Extensions;
using PlayPrism.Contracts.V1.Requests.Auth;
using PlayPrism.Contracts.V1.Responses.Auth;
using PlayPrism.Core.Domain;

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
    public async Task<IActionResult> Login([FromBody] AuthRequest request, CancellationToken cancellationToken)
    {
        var responseDto =
            await _accountService.LoginAsync(request.Email, request.Password, cancellationToken);

        if (responseDto == null)
        {
            return Unauthorized("Failed to login".ToErrorResponse());
        }


        SetRefreshTokenCookie(responseDto.RefreshToken);

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
    public async Task<IActionResult> Register([FromBody] AuthRequest request, CancellationToken cancellationToken)
    {
        var regDto =
            await _accountService.RegisterAsync(request.Email, request.Password,
                cancellationToken);

        if (regDto == null)
        {
            return Unauthorized($"User with such email {request.Email} already exists.".ToErrorResponse());
        }

        SetRefreshTokenCookie(regDto.RefreshToken);

        var response = new AuthResponse
        {
            Role = regDto.Role,
            UserId = regDto.UserId,
            Email = regDto.Email,
            AccessToken = regDto.AccessToken,
        };

        return Ok(response.ToApiResponse());
    }

    private void SetRefreshTokenCookie(RefreshToken newRefreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.ExpireDate
        };
        Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
    }

    //
    // [HttpGet]
    // [Route("logout")]
    // public Task<IActionResult> Logout(CancellationToken cancellationToken)
    // {
    //     
    // }
}