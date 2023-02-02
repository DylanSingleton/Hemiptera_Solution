using Hemiptera_API.Helpers;
using Hemiptera_API.Repositorys;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Services.Interfaces;

namespace Hemiptera_API.Services;

public static class DependencyInjectionService
{
    public static void RegisterDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();
        services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}