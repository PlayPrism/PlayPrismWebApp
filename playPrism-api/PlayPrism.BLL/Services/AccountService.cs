using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.V1.Responses.Auth;
using PlayPrism.Core.Domain;
using PlayPrism.DAL.Abstractions.Interfaces;

namespace PlayPrism.BLL.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AccountService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    public async Task<LoginResponse> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        var passwordHash = GetPasswordHash(password);
            
        var users = await _unitOfWork.Users
            .GetByConditionAsync(profile => profile.Email == username && profile.Password == passwordHash);

        var user = users.FirstOrDefault();

        if (user != null)
        {
            var token = CreateToken(user);
            var response = new LoginResponse()
            {
                Role = user.Role,
                UserId = user.Id,
                UserName = user.Email,
                Token = token
            };
            return response;
        }

        return null;
    }


    public async Task<LoginResponse> RegisterAsync(string username, string password, CancellationToken cancellationToken)
    {
        var passwordHash = GetPasswordHash(password);

        var isUserPresent = await _unitOfWork.Users.ExistAsync(user => user.Email == username && user.Password == passwordHash, cancellationToken);

        if (isUserPresent)
        {
            return new LoginResponse();
        }

        var user = new UserProfile()
        {
            Email = username,
            Password = passwordHash,
            Nickname = username.ToUpperInvariant(),
            Role = 0
        };
        
        await _unitOfWork.Users.AddAsync(user, cancellationToken);
        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.CommitAsync();
        await _unitOfWork.SaveAsync();

        var token = CreateToken(user);
        var response = new LoginResponse()
        {
            Role = user.Role,
            UserId = user.Id,
            UserName = user.Email,
            Token = token
        };

        return response;
    }

    private string CreateToken(UserProfile user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, Enum.GetName(user.Role))
        };

        var conf = _configuration["TokenKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(20),
            SigningCredentials = creds,
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    private string GetPasswordHash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);

        using var sha = new HMACSHA512(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
        var hash = sha.ComputeHash(bytes);

        var res = Encoding.UTF8.GetString(hash);
        return res;
    }
}