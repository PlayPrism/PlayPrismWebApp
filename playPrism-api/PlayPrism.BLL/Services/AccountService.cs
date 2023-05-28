using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Bogus.DataSets;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.BLL.Abstractions.Interfaces;
using PlayPrism.BLL.Constants;
using PlayPrism.Core.Domain;
using PlayPrism.Core.DTOs;
using PlayPrism.Core.Settings;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services;

/// <inheritdoc />
public class AccountService : IAccountService
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;

    public AccountService(
        ITokenService tokenService,
        IOptions<JwtSettings> jwtOptions,
        IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _jwtSettings = jwtOptions.Value;
    }

    /// <inheritdoc />
    public async Task<AuthDTO> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Users
            .GetByConditionAsync(profile => profile.Email == username, cancellationToken: cancellationToken);

        var user = users.FirstOrDefault();
        if (user == null)
        {
            return null;
        }

        var hash = GetHash(password);

        var verificationResult = hash == user.Password;

        if (verificationResult != true) return null;

        var tokens = await _unitOfWork.RefreshTokens.GetByConditionAsync(x => x.User.Id == user.Id,
            EntitiesSelectors.RefreshTokenSelector,
            cancellationToken);
        if (tokens != null && tokens.Count > 0)
        {
            var tokenToDelete = tokens.FirstOrDefault();
            _unitOfWork.RefreshTokens.Delete(tokenToDelete);
            await _unitOfWork.SaveAsync();
        }

        var claims = GetUserClaims(user);
        var token = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken(user);


        try
        {
            var trans = _unitOfWork.CreateTransactionAsync();

            user.RefreshToken = refreshToken;

            await _unitOfWork.RefreshTokens.AddAsync(refreshToken, cancellationToken);

            await _unitOfWork.Users.Update(user);

            await _unitOfWork.CommitTransactionAsync();
            await _unitOfWork.SaveAsync();

            var response = new AuthDTO()
            {
                Role = user.Role,
                UserId = user.Id,
                Email = user.Email,
                AccessToken = token,
                RefreshToken = refreshToken
            };
            return response;
        }

        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<AuthDTO> RegisterAsync(string email, string password, CancellationToken cancellationToken)
    {
        try
        {
            var passwordHash = GetHash(password);

            var isUserPresent = await _unitOfWork.Users.ExistAsync(user => user.Email == email, cancellationToken);

            if (isUserPresent)
            {
                return null;
            }

            var user = new UserProfile()
            {
                Email = email,
                Password = passwordHash,
                Role = 0
            };

            var claims = GetUserClaims(user);

            var accessToken = _tokenService.GenerateAccessToken(claims);

            var refreshToken = _tokenService.GenerateRefreshToken(user);

            user.RefreshToken = refreshToken;


            var trans = await _unitOfWork.CreateTransactionAsync();

            await _unitOfWork.Users.AddAsync(user, cancellationToken);


            await _unitOfWork.RefreshTokens.AddAsync(refreshToken, cancellationToken);

            var response = new AuthDTO
            {
                Role = user.Role,
                UserId = user.Id,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            await _unitOfWork.CommitTransactionAsync();
            await _unitOfWork.SaveAsync();
            return response;
        }

        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<AuthDTO> RefreshAuth(string accessToken, string refreshToken, CancellationToken cancellationToken)
    {
        try
        {
            var inputToken = accessToken.Replace("Bearer", "").Trim();
            var principal = _tokenService.GetPrincipalFromExpiredToken(inputToken);

            var emailClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

            var email = emailClaim?.Value;

            if (string.IsNullOrEmpty(email)) return null;

            var users = await _unitOfWork.Users
                .GetByConditionAsync(x => x.Email == email,
                    cancellationToken: cancellationToken);

            if (users.Count == 0)
                return null;

            var user = users.FirstOrDefault();

            var refreshTokens = await _unitOfWork.RefreshTokens
                .GetByConditionAsync(x => x.UserId == user.Id, EntitiesSelectors.RefreshTokenSelector,
                    cancellationToken: cancellationToken);

            if (refreshTokens.Count != 0)
            {
                var refreshTokenOld = refreshTokens.FirstOrDefault();

                if (refreshTokenOld?.Token != refreshToken)
                    return null;
                var now = DateTime.UtcNow;

                if (refreshTokenOld?.ExpireDate < now)
                {
                    _unitOfWork.RefreshTokens.Delete(refreshTokenOld);
                    await _unitOfWork.SaveAsync();
                    return null;
                }

                
                foreach (var token in refreshTokens)
                {
                    _unitOfWork.RefreshTokens.Delete(token);
                }
                
                await _unitOfWork.SaveAsync();
            }


            var newRefreshToken = _tokenService.GenerateRefreshToken(user);

            var claims = GetUserClaims(user);

            var newAccessToken = _tokenService.GenerateAccessToken(claims);

            user.RefreshToken = newRefreshToken;

            try
            {
                var trans = await _unitOfWork.CreateTransactionAsync();

                await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken, cancellationToken);

                await _unitOfWork.Users.Update(user);

                await _unitOfWork.CommitTransactionAsync();
                await _unitOfWork.SaveAsync();

                var res = new AuthDTO()
                {
                    Email = user.Email,
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    UserId = user.Id,
                    Role = user.Role
                };

                return res;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return null;
            }
        }
        catch (SecurityTokenException securityException)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    private List<Claim> GetUserClaims(UserProfile user)
    {
        var result = new List<Claim>(new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Nickname ??= string.Empty),
            new Claim(ClaimTypes.Role, Enum.GetName(user.Role)),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user?.Nickname)
        });

        return result;
    }

    private string GetHash(string password)
    {
        var passBytes = Encoding.UTF8.GetBytes(password);

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var hash = hmac.ComputeHash(passBytes);

        var res = Encoding.UTF8.GetString(hash);

        return res;
    }
}