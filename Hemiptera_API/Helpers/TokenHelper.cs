using Hemiptera_API.Repositorys.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Hemiptera_Contracts.Authentications.Responses;

namespace Hemiptera_API.Helpers
{
    public static class TokenHelper
    {
        public static AuthenticationResponse MapAuthResponse(
            List<Claim> claims,
            IRefreshTokenRepository refreshTokenRepository,
            IResponseCookies responseCookies)
        {
            var authResponse = new AuthenticationResponse(
                JwtHelper.GenerateAccessToken(claims),
                JwtHelper.GenerateRefreshToken());

            SetAccessToken(authResponse.AccessToken, responseCookies);
            SetRefreshToken(claims, authResponse.RefreshToken, refreshTokenRepository, responseCookies);

            return authResponse;
        }

        private static void SetAccessToken(string accessToken, IResponseCookies responseCookies)
        {
            responseCookies.Append("accessToken", accessToken);
        }

        private static void SetRefreshToken(
            List<Claim> claims,
            string refreshToken,
            IRefreshTokenRepository refreshTokenRepository,
            IResponseCookies responseCookies)
        {
            refreshTokenRepository.SetRefreshToken(claims, refreshToken);

            responseCookies.Append("refreshToken", refreshToken);
        }
    }
}