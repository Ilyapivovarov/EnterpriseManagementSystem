namespace ApiGateway.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string GetServiceUrl(this IConfiguration configuration, string name)
    {
        return configuration.GetRequiredSection("ServiceUrls")[name]!;
    }
}