using EnterpriseManagementSystem.JwtAuthorization;

namespace EmailService.Application;

public static class ApplicationDependencyInjection
{
    public static void AddAppication(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddJwtAuthorization(configuration);
    }
}