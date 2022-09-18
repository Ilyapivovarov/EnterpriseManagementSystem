namespace IdentityService.Infrastructure.Extensions;

public static class IWebHostEnvironmentExtensions
{
    public static bool IsTesting(this IWebHostEnvironment environment)
    {
        return environment.IsEnvironment("Testing");
    }
}