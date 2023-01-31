using System.Text.Json;
using StackExchange.Redis;

namespace IdentityService.Infrastructure.Services.CacheServices;

public sealed class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<object?> GetAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var value = await db.StringGetAsync(key);

        return JsonSerializer.Deserialize<object>(value);
    }

    public async Task<T?> TryGetAsync<T>(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var value = await db.StringGetAsync(key);

        return JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetAsync(string key, object value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, JsonSerializer.Serialize(value));
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, JsonSerializer.Serialize(value));
    }
    
    public async Task SetAsync<T>(string key, T value, DateTime expiry)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, JsonSerializer.Serialize(value), expiry - DateTime.Now);
    }
}