using Microsoft.EntityFrameworkCore;

namespace IdentityService;

public static class EnvHandler
{
    public static void ValidateDbBeforeStartApp(IdentityDbContext identityDbContext)
    {
        identityDbContext.Database.Migrate();
    }
    
    public static void ValidateCacheBeforeStartApp(ICacheService cacheService)
    {
        var canConnect = cacheService.CanConnect();

        if (!canConnect)
        {
            throw new Exception("No connections");
        }
    }
}
