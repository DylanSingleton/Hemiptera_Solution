using Hemiptera_API.Helpers;
using Hemiptera_API.Models;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;
using Hemiptera_Contracts.Authentication.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hemiptera_API.Controllers
{
    [Route("api/[controller]/")]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationService;
        private readonly JwtHelper _jwtHelper;
        private readonly IUnitOfWorkRepository _unitOfWork;

        public AuthenticationsController(
            IAuthenticationRepository authenticationService,
            JwtHelper jwtHelper,
            IUnitOfWorkRepository unitOfWork)
        {
            _authenticationService = authenticationService;
            _jwtHelper = jwtHelper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var validator = new LoginRequestValidator();
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                var loginResult = await _authenticationService.LoginAsync(request);

                if (loginResult.IsSuccessful)
                {
                    return Ok(SetTokens(loginResult.Payload));
                }

                return new ObjectResult(loginResult.Error)
                { StatusCode = (int)loginResult.Error!.HttpStatusCode };
            }
            return BadRequest(validationResult.Errors);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var validator = new RegisterRequestValidator();

            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                var registerResult = await _authenticationService.Register(request);
                if (registerResult.IsSuccessful)
                {
                    return Ok(SetTokens(registerResult.Payload));
                }

                return new ObjectResult(registerResult.Error)
                { StatusCode = (int)registerResult.Error!.HttpStatusCode };
            }
            return BadRequest(validationResult.Errors);
        }

        private AuthenticationResponse SetTokens(List<Claim> claims)
        {
            var authResponse = new AuthenticationResponse(
            _jwtHelper.GenerateAccessToken(claims),
            _jwtHelper.GenerateRefreshToken());

            var claimId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier!);
            var userGuid = Guid.Parse(claimId.ToString());

            _unitOfWork.RefreshToken.Create(RefreshToken.From(userGuid, authResponse.RefreshToken));
            _unitOfWork.Save();

            return authResponse;
        }
    }
}