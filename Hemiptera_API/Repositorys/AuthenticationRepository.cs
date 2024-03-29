﻿using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Hemiptera_Contracts.Authentications.Requests;

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

    public async Task<Result<List<Claim>>> LoginAsync(LoginRequest request)
    {
        // Find the user by email using the user manager
        var user = await _userManager.FindByEmailAsync(request.Email);

        // If the user is not found
        if (user is null)
        {
            // Return a failure message
            return new ErrorResult<List<Claim>>("Invalid email or password");
        }

        // Check the password using the sign-in manager
        var authResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        // If the password is correct
        if (authResult.Succeeded)
        {
            // Generate a token and return it along with a success message
            return new SuccessResult<List<Claim>>(PopulateUserClaims(user));
        }

        // If the password is incorrect, return a failure message
        return new ErrorResult<List<Claim>>("Invalid email or password");
    }

    public async Task<Result<List<Claim>>> Register(RegisterRequest request)
    {
        // Create a new user object with the email and username from the request
        var userToCreate = new User { Email = request.Email, UserName = request.UserName };

        // Use the user manager to create the new user and store it in the database
        var createdUser = await _userManager.CreateAsync(userToCreate, request.Password);

        // If the user was successfully created
        if (createdUser.Succeeded)
        {
            // Generate a token and return it along with a success message
            var claims = PopulateUserClaims(userToCreate);
            return new SuccessResult<List<Claim>>(claims);
        }

        // If there was an error creating the user, return a failure message
        return new ErrorResult<List<Claim>>("Invalid email or password");
    }

    private List<Claim> PopulateUserClaims(User user)
    {
        return new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!)
        };
    }
}