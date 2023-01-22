﻿using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Hemiptera_API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest request)
        {
            var validator = new LoginRequestValidator();
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                var getAuthResult = _authenticationService.Login(request);
                if (getAuthResult.IsSuccessful)
                {
                    return Ok(getAuthResult.Payload);
                }
            }
            return BadRequest(validationResult.Errors);
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest request)
        {
            var validator = new RegisterRequestValidator();

            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                return Ok(validationResult);
            }
            return BadRequest(validationResult.Errors);
        }
    }
}