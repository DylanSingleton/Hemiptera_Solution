using Hemiptera_API.Models;
using Hemiptera_API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
