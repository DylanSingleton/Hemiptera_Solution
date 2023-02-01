using Hemiptera_API.Models;
using Microsoft.AspNetCore.Identity;

namespace Hemiptera_API.Services;

public static class IdentityService
{
    public static void RegisterIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
         .AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();
    }
}