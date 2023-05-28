using PlayPrism.Contracts.V1.Responses.Auth;
using PlayPrism.Core.DTOs;

namespace PlayPrism.BLL.Abstractions.Interface;

public interface IAccountService
{
    Task<AuthDTO> LoginAsync(string username, string password,  CancellationToken cancellationToken);

    Task<AuthDTO> RegisterAsync(string username, string password, CancellationToken cancellationToken);

    Task<AuthDTO> RefreshAuth(string accessToken, CancellationToken cancellationToken);
}