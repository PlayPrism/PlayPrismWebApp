using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.BLL.Helpers;
using PlayPrism.Contracts.Extensions;
using PlayPrism.Contracts.V1.Requests.Auth;
using PlayPrism.Contracts.V1.Responses.Auth;
using PlayPrism.Core.Domain;
using Stripe;

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

    [HttpPost("login")]
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

    [HttpPost("register")]
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

    [HttpGet("refresh")]
    public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
    {
        try
        {
            var token = Request.Headers.Authorization;

            var responseDto =
                await _accountService.RefreshAuth(token, Request.Cookies["refreshToken"], cancellationToken);

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
        catch (Exception e)
        {
            return BadRequest("Something went wrong".ToErrorResponse());
        }
    }

    [HttpPost("requestPasswordReset")]
    public async Task<IActionResult> RequestPasswordReset([FromBody] string email, CancellationToken cancellationToken)
    {
        var res = await _accountService.RequestPasswordRefresh(email);
        
        if (res == false)
        {
            return BadRequest("Failed to send new refresh code".ToErrorResponse());
        }

        return Ok();
    }
    
    [HttpPost("verifyCode")]
    public async Task<IActionResult> VerifyCode([FromBody] EmailWithCodeRequest request, CancellationToken cancellationToken)
    {
        var res = await _accountService.VerifyCode(request);

        if (res == false)
        {
            return BadRequest("Code is not valid".ToErrorResponse());
        }

        return Ok();
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetUserPassword([FromBody] AuthRequest request, CancellationToken cancellationToken)
    {
        var res = await _accountService.ResetUserPassword(request);

        if (res == false)
        {
            return BadRequest("Failed to refresh password".ToErrorResponse());
        }

        return Ok();
    }

    private void SetRefreshTokenCookie(RefreshToken newRefreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.ExpireDate
        };
        Response.Cookies.Delete("refreshToken");
        Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
    }
}