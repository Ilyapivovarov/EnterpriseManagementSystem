using ApiGateway.Application.HttpClients;
using ApiGateway.Infrastructure.Handlers;
using ApiGateway.Infrastructure.HttpClients;
using EnterpriseManagementSystem.JwtAuthorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiGateway.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        serviceProvider.AddHttpContextAccessor();
        serviceProvider.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        
        serviceProvider.AddHttpClient<IAuthHttpClient, AuthHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["IdentityServiceUrl"]);
            })
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        #region Register Jwt auth

        serviceProvider.AddJwtAuthorization(configuration);

        #endregion
    }
}