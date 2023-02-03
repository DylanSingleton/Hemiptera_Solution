using Hemiptera_API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Hemiptera_API.Services;

public static class AuthenticationService
{
    public static void RegisterAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = JwtSettings.ValidateIssuer,
                    ValidateAudience = JwtSettings.ValidateAudience,
                    ValidateLifetime = JwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = JwtSettings.ValidateIssuerSigningKey,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidAudience = JwtSettings.Audience,
                    IssuerSigningKey = JwtSettings.IssuerSigningKey
                };
            });
    }
}