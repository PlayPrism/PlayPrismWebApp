using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.BLL.Abstractions.Interfaces;
using PlayPrism.BLL.Constants;
using PlayPrism.BLL.Helpers;
using PlayPrism.Contracts.V1.Requests.Auth;
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
    private readonly EmailWorker _emailWorker;
    private readonly AppSettings _appSettings;

    public AccountService(
        ITokenService tokenService,
        IOptions<AppSettings> appSettings,
        IUnitOfWork unitOfWork,
        EmailWorker emailWorker)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _emailWorker = emailWorker;
        _appSettings = appSettings?.Value;
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

    public async Task<bool> RequestPasswordRefresh(string email)
    {
        var random = new Random();
        var code = random.Next(100000, 999999).ToString(CultureInfo.InvariantCulture);

        try
        {
            var codes = await _unitOfWork.RefreshCodes.GetByConditionAsync(x => x.User.Email == email);

            foreach (var item in codes)
            {
                _unitOfWork.RefreshCodes.Delete(item);
                await _unitOfWork.SaveAsync();
            }

            var users = await _unitOfWork.Users.GetByConditionAsync(x => x.Email == email);

            if (users.Count == 0)
                return false;

            var user = users.FirstOrDefault();

            var refreshCode = new ResetPasswordCode()
            {
                User = user,
                ProfileUserId = user.Id,
                Code = code
            };

            await _unitOfWork.RefreshCodes.AddAsync(refreshCode);

            await _emailWorker.SendVerificationCodeByEmailAsync(email, code);

            await _unitOfWork.SaveAsync();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> ResetUserPassword(AuthRequest request)
    {
        var users = await _unitOfWork.Users.GetByConditionAsync(x => x.Email == request.Email);

        if (users.Count > 0)
            return false;

        var user = users.FirstOrDefault();

        user.Password = request.Password;

        await _unitOfWork.Users.Update(user);

        await _unitOfWork.SaveAsync();

        var codes = await _unitOfWork.RefreshCodes.GetByConditionAsync(x => x.ProfileUserId == user.Id);

        if (codes.Count == 0)
            return true;

        foreach (var refreshCode in codes)
        {
            _unitOfWork.RefreshCodes.Delete(refreshCode);
            await _unitOfWork.SaveAsync();
        }

        return true;
    }

    public async Task<bool> VerifyCode(EmailWithCodeRequest request)
    {
        var users = await _unitOfWork.Users.GetByConditionAsync(x => x.Email == request.Email);

        if (users.Count > 0)
            return false;

        var user = users.FirstOrDefault();

        var codes = await _unitOfWork.RefreshCodes.GetByConditionAsync(x => x.Code == request.Code);

        if (codes.Count == 0)
            return false;

        var code = codes.FirstOrDefault();
        
        var comparison = code.ProfileUserId == user.Id && code.ExpirationDate > DateTime.UtcNow;

        return comparison;
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

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_appSettings.JwtSettings.Key));

        var hash = hmac.ComputeHash(passBytes);

        var res = Encoding.UTF8.GetString(hash);

        return res;
    }
}