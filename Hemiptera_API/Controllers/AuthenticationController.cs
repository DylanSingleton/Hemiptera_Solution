using Hemiptera_API.Models;
using Hemiptera_API.Services;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;
using Hemiptera_Contracts.Authentication.Validators;
using Hemiptera_Contracts.Project.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Hemiptera_API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationService;
        private readonly IUnitOfWorkRepository _unitOfWork;

        public AuthenticationController(
            IAuthenticationRepository authenticationService,
            IUnitOfWorkRepository unitOfWork)
        {
            _authenticationService = authenticationService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var validator = new LoginRequestValidator();
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                var getAuthResult = await _authenticationService.LoginAsync(request);
                if (getAuthResult.IsSuccessful)
                {
                    return Ok(MapAuthenticationResponse(getAuthResult, GetRefreshToken()));
                }

                return new ObjectResult(getAuthResult.Error)
                { StatusCode = (int)getAuthResult.Error!.HttpStatusCode };
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
                var getAuthResult = await _authenticationService.Register(request);
                if (getAuthResult.IsSuccessful)
                {
                    return Ok(
                        MapAuthenticationResponse(getAuthResult, GetRefreshToken()));
                }

                return new ObjectResult(getAuthResult.Error)
                { StatusCode = (int)getAuthResult.Error!.HttpStatusCode };
            }
            return BadRequest(validationResult.Errors);
        }

        private RefreshToken GetRefreshToken()
        {
            var refreshToken = _unitOfWork.RefreshToken.GenerateRefreshToken();
            _unitOfWork.RefreshToken.Insert(refreshToken);
            _unitOfWork.Save();
            return refreshToken;
        }
        private static AuthenticationResponse MapAuthenticationResponse(ServiceResultWithPayload<string> accessToken, RefreshToken refreshToken)
        {
            return new AuthenticationResponse(
                accessToken.Payload!,
                refreshToken.Token);
        }
    }
}