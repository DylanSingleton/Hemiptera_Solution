using Hemiptera_API.Extensions;
using Hemiptera_API.Helpers;
using Hemiptera_API.Models;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;
using Hemiptera_Contracts.Authentication.Validators;
using Hemiptera_Contracts.Project.Validator;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hemiptera_API.Controllers;

[Route("api/[controller]/")]
public class AuthenticationsController : ControllerBase
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly JwtHelper _jwtHelper;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWorkRepository _unitOfWork;

    public AuthenticationsController(
        IAuthenticationRepository authenticationRepository,
        JwtHelper jwtHelper,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWorkRepository unitOfWork)
    {
        _authenticationRepository = authenticationRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtHelper = jwtHelper;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var validationResult = request.Validate(new LoginRequestValidator());
        if (validationResult.IsUnsuccessful) return BadRequest(validationResult.Errors);

        var loginResult = await _authenticationRepository.LoginAsync(request);

        if (loginResult.IsSuccessful)
        {
            return Ok(SetTokens(loginResult.Payload));
        }
        else if (loginResult is ErrorResult<List<Claim>> errorResult)
        {
            return BadRequest(errorResult.Message);
        }
        return BadRequest();
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var validationResult = request.Validate(new RegisterRequestValidator());
        if (validationResult.IsUnsuccessful) return BadRequest(validationResult.Errors);

        var registerResult = await _authenticationRepository.Register(request);
        if (registerResult.IsSuccessful)
        {
            return Ok(SetTokens(registerResult.Payload));
        }

        return BadRequest();
    }

    private AuthenticationResponse SetTokens(List<Claim> claims)
    {
        var authResponse = new AuthenticationResponse(
        _jwtHelper.GenerateAccessToken(claims),
        _jwtHelper.GenerateRefreshToken());

        var userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier.ToString());
        var userGuid = Guid.Parse(userId!.Value);
        _refreshTokenRepository.RevokeRefreshToken(userGuid);

        _unitOfWork.RefreshToken.Create(RefreshToken.From(userGuid, authResponse.RefreshToken));
        _unitOfWork.Save();

        return authResponse;
    }
}