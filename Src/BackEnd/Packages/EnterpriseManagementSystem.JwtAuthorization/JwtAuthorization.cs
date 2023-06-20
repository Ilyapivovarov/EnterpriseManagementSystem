using EnterpriseManagementSystem.JwtAuthorization.Infrastructure;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using EnterpriseManagementSystem.JwtAuthorization.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseManagementSystem.JwtAuthorization;

public static class JwtAuthorization
{
    public static void AddJwtAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        
        services.AddTransient<IJwtSessionService, JwtSessionService>();
    }
}
