using System.Security.Claims;

namespace PlayPrism.BLL.Abstractions.Interfaces;

/// <summary>
/// Token service.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates acess token.
    /// </summary>
    /// <param name="claims">List of claims.</param>
    /// <returns></returns>
    string GenerateAccessToken(IList<Claim> claims);

    /// <summary>
    /// Generates refresh token.
    /// </summary>
    /// <returns></returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Gets principal form expired token.
    /// </summary>
    /// <param name="token">The expired token.</param>
    /// <returns></returns>
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}