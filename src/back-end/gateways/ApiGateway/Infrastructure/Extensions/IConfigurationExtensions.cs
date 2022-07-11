namespace ApiGateway.Infrastructure.Extensions;

public static class IConfigurationExtensions
{
    public static string GetServiceUrl(this IConfiguration configuration, string name)
    {
        return configuration.GetSection("ServiceUrls")[name];
    }
}