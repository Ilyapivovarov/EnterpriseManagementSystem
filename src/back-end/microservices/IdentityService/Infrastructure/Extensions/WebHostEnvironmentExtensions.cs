namespace IdentityService.Infrastructure.Extensions;

public static class WebHostEnvironmentExtensions
{
    public static bool IsTesting(this IWebHostEnvironment environment)
    {
        return environment.IsEnvironment("Testing");
    }
}