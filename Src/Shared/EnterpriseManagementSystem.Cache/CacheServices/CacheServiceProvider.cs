using StackExchange.Redis;

namespace EnterpriseManagementSystem.Cache.CacheServices;

public class CacheServiceProvider : ICacheServiceProvider
{
    private readonly CacheServiceConfiguration _options;

    public CacheServiceProvider(IOptions<CacheServiceConfiguration> options)
    {
        _options = options.Value;
    }

    public ICacheService UseCache()
    {
        return new RedisCacheService(ConnectionMultiplexer.Connect(_options.ConnectionString));
    }
}
