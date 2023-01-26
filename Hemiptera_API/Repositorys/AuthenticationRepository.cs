using Hemiptera_API.Models;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_API.Services.Service_Errors;
using Hemiptera_API.Settings;
using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Hemiptera_API.Services;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthenticationRepository(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<OperationResultWithPayload<List<Claim>>> LoginAsync(LoginRequest request)
    {
        // Find the user by email using the user manager
        var user = await _userManager.FindByEmailAsync(request.Email);

        // If the user is not found
        if (user is null)
        {
            // Return a failure message
            return new OperationResultWithPayload<List<Claim>>(
                    new FailedAuthOperationError());
        }

        // Check the password using the sign-in manager
        var authResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        // If the password is correct
        if (authResult.Succeeded)
        {
            
            // Generate a token and return it along with a success message
            return new OperationResultWithPayload<List<Claim>>(new List<Claim> { });
        }

        // If the password is incorrect, return a failure message
        return new OperationResultWithPayload<List<Claim>>(
                    new FailedAuthOperationError());
    }

    public async Task<OperationResult> Register(RegisterRequest request)
    {
        // Create a new user object with the email and username from the request
        var userToCreate = new User { Email = request.Email, UserName = request.UserName };

        // Use the user manager to create the new user and store it in the database
        var createdUser = await _userManager.CreateAsync(userToCreate, request.Password);

        // If the user was successfully created
        if (createdUser.Succeeded)
        {
            // Generate a token and return it along with a success message

        }

        // If there was an error creating the user, return a failure message
        return new OperationResult(
                                new FailedAuthOperationError());
    }
}