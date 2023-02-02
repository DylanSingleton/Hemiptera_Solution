using Hemiptera_API.Helpers;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var refreshCookie = Request.Cookies["refreshToken"];
            var accessCookie = Request.Cookies["accessToken"];
            if (refreshCookie is not null && accessCookie is not null)
            {
                var tokenResult = _refreshTokenRepository.ValidateRefreshToken(refreshCookie);
                if (tokenResult.IsSuccessful)
                {
                    var claims = JwtHelper.GetClaimsFromAccessToken(accessCookie);

                    TokenHelper.MapAuthResponse(claims, _refreshTokenRepository, Response.Cookies);
                    return Ok();
                }
                else if (tokenResult is ErrorResult errorResult)
                {
                    return Unauthorized(errorResult.Message);
                }
            }
            return Unauthorized();
        }
    }
}