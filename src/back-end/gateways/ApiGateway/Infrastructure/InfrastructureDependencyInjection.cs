using EnterpriseManagementSystem.JwtAuthorization;

namespace ApiGateway.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        #region Register HttpClients

        services.AddHttpContextAccessor();
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IIdentityServiceHttpClient, IdentityServiceHttpClient>(client =>
                client.BaseAddress = new Uri(configuration.GetServiceUrl("IdentityServiceUrl")))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ITaskServiceHttpClient, TaskServiceHttpClient>(client =>
                client.BaseAddress = new Uri(configuration.GetServiceUrl("TaskServiceUrl")))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IUserServiceHttpClient, UserServiceHttpClient>(client =>
                client.BaseAddress = new Uri(configuration.GetServiceUrl("UserServiceUrl")))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        #endregion

        #region Register Jwt auth

        services.AddJwtAuthorization(configuration);

        #endregion
    }
}