using EnterpriseManagementSystem.JwtAuthorization;

namespace ApiGateway.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IIdentityHttpClient, IdentityHttpClient>(client =>
                client.BaseAddress = new Uri(configuration.GetServiceUrl("IdentityServiceUrl")))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ITaskServiceHttpClient, TaskServiceHttpClient>(client =>
                client.BaseAddress = new Uri(configuration.GetServiceUrl("TaskServiceUrl")))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        #region Register Jwt auth

        services.AddJwtAuthorization(configuration);

        #endregion

        services.AddCors();
    }
}