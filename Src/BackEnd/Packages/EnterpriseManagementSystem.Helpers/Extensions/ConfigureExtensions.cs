using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EnterpriseManagementSystem.Helpers.Extensions;

public static class ConfigurationExtensions
{
    public static string GetRequiredConnectionString(this IConfiguration configuration, string name)
    {
        var connectionString = configuration.GetConnectionString(name);
        if (connectionString == null)
        {
            throw new NullReferenceException($"Not found connection with name {name}");
        }

        return connectionString;
    }
}