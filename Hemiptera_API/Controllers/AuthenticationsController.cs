using Hemiptera_API.Extensions;
using Hemiptera_API.Helpers;
using Hemiptera_API.Models;
using Hemiptera_API.Repositorys;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_API.Utilitys;
using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Hemiptera_API.Validators.Authentications;

namespace Hemiptera_API.Controllers;

[Route("api/[controller]/")]
public class AuthenticationsController : ControllerBase
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthenticationsController(
        IAuthenticationRepository authenticationRepository,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _authenticationRepository = authenticationRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var validatorResult = ValidatorResultUtility.Validate(request, new LoginRequestValidator());
        if (validatorResult.IsUnsuccessful) return BadRequest(validatorResult.Errors);

        var loginResult = await _authenticationRepository.LoginAsync(request);
        if (loginResult.IsSuccessful)
        {
            var response = TokenHelper.MapAuthResponse(loginResult.Payload, _refreshTokenRepository, Response.Cookies);
            return Ok(response);
        }
        if (loginResult is ErrorResult<List<Claim>> errorResult)
        {
            return BadRequest(errorResult.Message);
        }

        return BadRequest();
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var validatorResult = ValidatorResultUtility.Validate(request, new RegisterRequestValidator());
        if (validatorResult.IsUnsuccessful) return BadRequest(validatorResult.Errors);

        var registerResult = await _authenticationRepository.Register(request);
        if (!registerResult.IsSuccessful) return BadRequest();
        var response = TokenHelper.MapAuthResponse(registerResult.Payload, _refreshTokenRepository, Response.Cookies);
        return Ok(response);

    }
}