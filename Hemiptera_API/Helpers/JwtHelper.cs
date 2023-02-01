using Hemiptera_API.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Hemiptera_API.Helpers;

public class JwtHelper
{
    public string GenerateAccessToken(List<Claim> claims)
    {
        // Use the signing credentials from the JwtSettings to create a new SigningCredentials object
        var credentials = new SigningCredentials(
            JwtSettings.IssuerSigningKey,
            SecurityAlgorithms.HmacSha256);

        // Create an array of claims that will be included in the JWT

        // Create a new JWT using the claims, expiration time, and signing credentials
        var token = new JwtSecurityToken(
            issuer: JwtSettings.Issuer,
            audience: JwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(JwtSettings.MinuteLifetime),
            signingCredentials: credentials);

        // Use the JwtSecurityTokenHandler to write the JWT to a string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}