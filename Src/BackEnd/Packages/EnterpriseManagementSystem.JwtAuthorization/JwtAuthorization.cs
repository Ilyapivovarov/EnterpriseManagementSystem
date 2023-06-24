using EnterpriseManagementSystem.JwtAuthorization.Abstractions;
using EnterpriseManagementSystem.JwtAuthorization.Infrastructure;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using EnterpriseManagementSystem.JwtAuthorization.Middlewares;
using EnterpriseManagementSystem.JwtAuthorization.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseManagementSystem.JwtAuthorization;

public static class JwtAuthorization
{
    public static void AddJwtAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddTransient<IJwtSessionService, JwtSessionService>();
        services.AddScoped<ICurrenSession, CurrenSession>();
        
        services.AddScoped<JwtAuthorizationMiddleware>();
    }

    public static void UseJwtAuthorizationMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<JwtAuthorizationMiddleware>();
    }
}