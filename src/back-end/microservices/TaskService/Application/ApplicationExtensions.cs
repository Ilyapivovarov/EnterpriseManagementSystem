using System.Reflection;

namespace TaskService.Application;

public static class ApplicationExtensions
{
    public static void AddApplication(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}