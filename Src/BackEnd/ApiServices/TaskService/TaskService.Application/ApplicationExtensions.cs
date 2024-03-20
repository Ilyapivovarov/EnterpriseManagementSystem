using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TaskService.Application;

public static class ApplicationExtensions
{
    public static void AddApplication(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        // services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        // services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}