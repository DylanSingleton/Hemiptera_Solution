using Hemiptera_API.Services.Interfaces;
using Hemiptera_API.Settings;
using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hemiptera_API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public ServiceResultWithPayload<AuthenticationResponse> Login(LoginRequest request)
        {
            var response = new AuthenticationResponse(GenerateToken());

            return new ServiceResultWithPayload<AuthenticationResponse>(response, true);
        }

        public ServiceResultWithPayload<AuthenticationResponse> Register(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        private string GenerateToken()
        {
            var credentials = new SigningCredentials(
                JwtSettings.IssuerSigningKey,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,"name"),
                new Claim(ClaimTypes.Role,"roles")
            };

            var token = new JwtSecurityToken(
                issuer: JwtSettings.Issuer,
                audience: JwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(JwtSettings.Lifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
    