using PlayPrism.Contracts.V1.Responses.Auth;

namespace PlayPrism.BLL.Abstractions.Interface;

public interface IAccountService
{
    Task<LoginResponse> LoginAsync(string username, string password,  CancellationToken cancellationToken);
    Task<LoginResponse> RegisterAsync(string username, string password, CancellationToken cancellationToken);
}