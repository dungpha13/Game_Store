using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameStore.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Api.Helpers;

public static class JwtHelper
{
    public static string CreateToken(User user, string secretKey)
    {
        List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}