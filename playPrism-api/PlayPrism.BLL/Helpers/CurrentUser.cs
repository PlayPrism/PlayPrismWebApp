using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using PlayPrism.Core.Domain;

namespace PlayPrism.BLL.Helpers;

public static class CurrentUser
{
    public static string GetCurrentUserFromToken(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Get the "sub" claim which represents the user ID or username
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");

            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
        }

        return null;
    }
}