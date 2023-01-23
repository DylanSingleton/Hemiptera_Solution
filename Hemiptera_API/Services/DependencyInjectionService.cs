using Hemiptera_API.Services.Interfaces;

namespace Hemiptera_API.Services;

public static class DependencyInjectionService
{
    public static void RegisterDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();
        services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
    }
}