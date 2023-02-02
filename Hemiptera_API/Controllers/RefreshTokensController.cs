using Hemiptera_API.Helpers;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hemiptera_API.Controllers
{
    [Route("api/[controller]/")]
    public class RefreshTokensController : ControllerBase
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokensController(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost("Refresh")]
        public IActionResult RefreshToken()
        {
            // Try to get the refresh and access tokens from the cookies
            if (!TryGetRefreshAndAccessTokens(out string refreshToken, out string accessToken))
            {
                // If either token is not found, return Unauthorized
                return Unauthorized();
            }

            // Validate the refresh token
            var tokenResult = _refreshTokenRepository.ValidateRefreshToken(refreshToken);
            if (!tokenResult.IsSuccessful)
            {
                // If the token is invalid, return Unauthorized with the error message
                return HandleErrorResult(tokenResult);
            }

            // Get the claims from the access token
            var claimsResult = JwtHelper.GetClaimsFromAccessToken(accessToken);
            if (!claimsResult.IsSuccessful)
            {
                // If the claims cannot be obtained, return Unauthorized with the error message
                return HandleErrorResult(claimsResult);
            }

            // Generate a new authentication response
            var response = TokenHelper.MapAuthResponse(claimsResult.Payload, _refreshTokenRepository, Response.Cookies);
            // Return the new response as a success
            return Ok(response);
        }

        private bool TryGetRefreshAndAccessTokens(out string refreshToken, out string accessToken)
        {
            // Get the refresh and access tokens from the cookies
            refreshToken = Request.Cookies["refreshToken"] ?? string.Empty;
            accessToken = Request.Cookies["accessToken"] ?? string.Empty;

            // Return whether both tokens were found
            return !string.IsNullOrEmpty(refreshToken) && !string.IsNullOrEmpty(accessToken);
        }

        private IActionResult HandleErrorResult(Result result)
        {
            // If the result is an error result, return Unauthorized with the error message
            if (result is ErrorResult errorResult)
            {
                return Unauthorized(errorResult.Message);
            }
            // Otherwise, return Unauthorized without a message
            return Unauthorized();
        }
    }
}
