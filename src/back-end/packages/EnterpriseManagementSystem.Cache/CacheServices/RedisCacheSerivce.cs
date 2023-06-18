using EnterpriseManagementSystem.Cache.Abstractions;
using StackExchange.Redis;

namespace EnterpriseManagementSystem.Cache.CacheServices;

public sealed class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }
    
    public async Task SetAsync(string key, string value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, value);
    }

    public async Task SetAsync(string key, string value, TimeSpan expiry)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, value, expiry);
    }

    public async Task SetAsync<TKey, TValue>(TKey key, TValue value, TimeSpan expiry)
    where TKey : notnull
    where TValue : notnull
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key.ToString(), value.ToString(), expiry);
    }

    public async Task<string?> GetStringAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var value = await db.StringGetAsync(key);

        return value.ToString();
    }

    public bool CanConnect()
    {
        return _connectionMultiplexer.IsConnected;
    }
}