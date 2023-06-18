using EnterpriseManagementSystem.Cache.CacheServices;
using EnterpriseManagementSystem.Cache.Common;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace EnterpriseManagementSystem.Cache.Abstractions;

public interface ICacheServiceProvider
{
    public ICacheService UseCache();
}

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