using Azure.Core;
using Hemiptera_API.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Hemiptera_API.Helpers;

public static class JwtHelper
{
    public static string GenerateAccessToken(List<Claim> claims)
    {
        // Use the signing credentials from the JwtSettings to create a new SigningCredentials object
        var credentials = new SigningCredentials(
            JwtSettings.IssuerSigningKey,
            SecurityAlgorithms.HmacSha256);

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

    public static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public static List<Claim> GetClaimsFromAccessToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadToken(accessToken) as JwtSecurityToken;
        var claims = token.Claims.ToList();
        return claims;
    }
}