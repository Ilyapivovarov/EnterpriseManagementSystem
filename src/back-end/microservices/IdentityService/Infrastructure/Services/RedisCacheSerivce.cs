using StackExchange.Redis;

namespace IdentityService.Infrastructure.Services;

public sealed class RedisCacheSerivce : ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisCacheSerivce(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<string> GetAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task SetAsync(string key, string value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, value);
    }
}