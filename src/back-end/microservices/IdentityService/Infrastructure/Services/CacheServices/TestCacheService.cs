using System.Collections.Concurrent;

namespace IdentityService.Infrastructure.Services.CacheServices;

public sealed class TestCacheService : ICacheService
{
    private readonly ConcurrentDictionary<string, string> _cache;

    public TestCacheService(IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsStaging())
            throw new Exception($"{nameof(TestCacheService)} can use only in {Environments.Staging} environment");

        _cache = new ConcurrentDictionary<string, string>();
    }

    public async Task SetAsync(string key, string value)
    {
        await Task.Run(() => _cache.TryAdd(key, value));
    }

    public async Task SetAsync(string key, string value, TimeSpan expiry)
    {
        await Task.Run(() => _cache.TryAdd(key, value));
    }

    public async Task SetAsync<TKey, TValue>(TKey key, TValue value, TimeSpan expiry)
        where TKey : notnull
        where TValue : notnull
    {
        await Task.Run(() => _cache.TryAdd(key.ToString()!, value.ToString()!));
    }

    public async Task<string?> GetStringAsync(string key)
    {
        return await Task.Run(() => _cache.GetValueOrDefault(key));
    }

    public bool CanConnect()
    {
        return true;
    }
}