using System.Reflection;
using EnterpriseManagementSystem.JwtAuthorization;
using MediatR;

namespace EmailService.Application;

public static class ApplicationDependencyInjection
{
    public static void AddAppication(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddJwtAuthorization(configuration);
    }
}