using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.BLL.Abstractions.Interfaces;
using PlayPrism.Core.Domain;
using PlayPrism.Core.DTOs;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services;

public class AccountService : IAccountService
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }
    public async Task<AuthDTO> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
         GetPasswordHash(password, out string passwordHash, out byte[] passwordSalt);
            
        var users = await _unitOfWork.Users
            .GetByConditionAsync(profile => profile.Email == username, cancellationToken: cancellationToken);
        
        //VerifyPasswordHash()

        var user = users.FirstOrDefault();

        if (user != null)
        {
            var claims = GetUserClaims(user);

            var token = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            
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

    public async Task<AuthDTO> RegisterAsync(string email, string password, CancellationToken cancellationToken)
    {
        GetPasswordHash(password, out string passwordHash, out byte[] passwordSalt );

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
        
        
        await _unitOfWork.BeginTransactionAsync();
        
        await _unitOfWork.Users.AddAsync(user, cancellationToken);

        await _unitOfWork.CommitAsync();
        await _unitOfWork.SaveAsync();

        var claims = GetUserClaims(user);

        var accessToken = _tokenService.GenerateAccessToken(claims);

        var refreshToken = _tokenService.GenerateRefreshToken();

        var response = new AuthDTO()
        {
            Role = user.Role,
            UserId = user.Id,
            Email = user.Email,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return response;
    }

    
    private List<Claim> GetUserClaims(UserProfile user)
    {
        var result = new List<Claim>(new []
        {
            new Claim(JwtRegisteredClaimNames.Jti, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Nickname),
            new Claim(ClaimTypes.Role, Enum.GetName(user.Role)),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Nickname)
        });

        return result;

    }


    private void GetPasswordHash(string password, out string passwordHash, out byte[] passwordSalt)
    {
        var passBytes = Encoding.UTF8.GetBytes(password);
        
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("my_super_key"));
        
        var hash = hmac.ComputeHash(passBytes);

        passwordSalt = hmac.Key;
        
        passwordHash = Encoding.UTF8.GetString(hash);

    }
    
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
    
    
    // private string CreateToken(UserProfile user, DateTime time)
    // {
    //     var claims = new List<Claim>()
    //     {
    //         new Claim(ClaimTypes.Email, user.Email),
    //         new Claim(ClaimTypes.Role, Enum.GetName(user.Role))
    //     };
    //
    //     var conf = _configuration["TokenKey"];
    //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf));
    //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    //     var tokenDescriptor = new SecurityTokenDescriptor()
    //     {
    //         Subject = new ClaimsIdentity(claims),
    //         Expires = DateTime.Now.AddMinutes(20),
    //         SigningCredentials = creds,
    //     };
    //     
    //     var tokenHandler = new JwtSecurityTokenHandler();
    //     
    //     var token = tokenHandler.CreateToken(tokenDescriptor);
    //
    //     return tokenHandler.WriteToken(token);
    // }
    
    // private string GetPasswordHash(string password, out var salt)
    // {
    //     var salt 
    //     var bytes = Encoding.UTF8.GetBytes(password);
    //
    //     using var sha = new HMACSHA512(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
    //     var hash = sha.ComputeHash(bytes);
    //
    //     var res = Encoding.UTF8.GetString(hash);
    //     return res;
    // }
    
    
    
}