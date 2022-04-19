using System.Reflection;
using EnterpriseManagementSystem.JwtAuthorization;

namespace IdentityService.Application;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        serviceProvider.AddMediatR(Assembly.GetExecutingAssembly());
        serviceProvider.AddAutoMapper(Assembly.GetExecutingAssembly());

        #region Register Jwt auth

        serviceProvider.AddJwtAuthorization(configuration);

        #endregion
    }
}