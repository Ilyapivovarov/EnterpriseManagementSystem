namespace ApiGateway.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        serviceProvider.AddHttpClient<IAuthHttpClientService, AuthHttpClientService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7064/");
        });
    }
}