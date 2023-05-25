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
    /// Initializes a new instance of the <see cref="AccountController"/> class.
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
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
       var response =  await _accountService.LoginAsync(request.Username, request.Password, cancellationToken: cancellationToken);
       if (response != null)
       {
           return Ok(response.ToApiResponse());
       }
       
       return Unauthorized(new LoginResponse().ToApiResponse());
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _accountService.RegisterAsync(request.Username, request.Password, cancellationToken: cancellationToken);
        if (response != null)
        {
            return Ok(response.ToApiResponse());
        }
       
        return Unauthorized(new LoginResponse().ToApiResponse());
    }
    //
    // [HttpGet]
    // [Route("logout")]
    // public Task<IActionResult> Logout(CancellationToken cancellationToken)
    // {
    //     
    // }
}