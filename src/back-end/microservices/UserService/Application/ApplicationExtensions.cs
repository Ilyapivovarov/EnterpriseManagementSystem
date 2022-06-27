using System.Reflection;

namespace UserService.Application;

public static class ApplicationExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}