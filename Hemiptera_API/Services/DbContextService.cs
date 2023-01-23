using Hemiptera_API.Models;
using Hemiptera_API.Settings;
using Microsoft.EntityFrameworkCore;

namespace Hemiptera_API.Services;

public static class DbContextService
{
    public static void RegisterDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(DbContextSettings.ConnectionString);
        });
    }
}