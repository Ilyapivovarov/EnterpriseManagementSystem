using System.Reflection;
using EnterpriseManagementSystem.JwtAuthorization;
using IdentityService.Application.Mediators.Handlers.Auth;
using IdentityService.Application.Mediators.Handlers.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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