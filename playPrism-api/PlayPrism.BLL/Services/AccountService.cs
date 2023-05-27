using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.BLL.Abstractions.Interfaces;
using PlayPrism.Core.Domain;
using PlayPrism.Core.DTOs;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services;

/// <inheritdoc />
public class AccountService : IAccountService
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
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

        var verificationResult = VerifyPasswordHash(password, user.Password, user.PasswordSalt);

        if (verificationResult)
        {
            var claims = GetUserClaims(user);
            var token = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken(user);
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

        return null;
    }

    /// <inheritdoc />
    public async Task<AuthDTO> RegisterAsync(string email, string password, CancellationToken cancellationToken)
    {
        try
        {
            GetPasswordHash(password, out string passwordHash, out byte[] passwordSalt);

            var isUserPresent = await _unitOfWork.Users.ExistAsync(user => user.Email == email, cancellationToken);

            if (isUserPresent)
            {
                return null;
            }

            var user = new UserProfile()
            {
                Email = email,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                Role = 0
            };

            await _unitOfWork.Users.AddAsync(user, cancellationToken);

            var claims = GetUserClaims(user);

            var accessToken = _tokenService.GenerateAccessToken(claims);

            var refreshToken = _tokenService.GenerateRefreshToken(user);

            var response = new AuthDTO
            {
                Role = user.Role,
                UserId = user.Id,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            
            await _unitOfWork.SaveAsync();
            return response;
        }
        
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
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
            new Claim(ClaimTypes.Name, user.Nickname)
        });

        return result;
    }

    private void GetPasswordHash(string password, out string passwordHash, out byte[] passwordSalt)
    {
        var passBytes = Encoding.UTF8.GetBytes(password);
        
        using var hmac = new HMACSHA256();
        
        passwordSalt = hmac.Key;
        
        var hash = hmac.ComputeHash(passBytes);

        passwordHash = Encoding.UTF8.GetString(hash);

    }

    private bool VerifyPasswordHash(string password, string passwordHash, byte[] passwordSalt)
    {

        byte[] hashBytes = Encoding.UTF8.GetBytes(passwordHash);

        using var hmac = new HMACSHA256(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        
        return computedHash.SequenceEqual(hashBytes);
    }
}