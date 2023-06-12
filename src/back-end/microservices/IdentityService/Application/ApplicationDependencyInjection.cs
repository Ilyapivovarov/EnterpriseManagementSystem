using System.Reflection;

namespace IdentityService.Application;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        serviceProvider.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        serviceProvider.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}