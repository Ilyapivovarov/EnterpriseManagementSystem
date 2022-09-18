using System.Collections.Concurrent;
using System.Text.Json;

namespace IdentityService.Infrastructure.Services.CacheServices;

public sealed class TestCacheService : ICacheService
{
    private readonly ConcurrentDictionary<string, string> _cache;

    public TestCacheService(IWebHostEnvironment webHostEnvironment)
    {
        if (!webHostEnvironment.IsTesting())
            throw new Exception($"{nameof(TestCacheService)} can use only in test environment");
        
        _cache = new ConcurrentDictionary<string, string>();
    }
    
    public async Task<object?> GetAsync(string key)
    {
        return await Task.Run(() =>
        {
            if (!_cache.TryGetValue(key, out var value))
                return default;

            return JsonSerializer.Deserialize<object>(value);
        });
    }

    public async Task<T?> TryGetAsync<T>(string key)
    {
        return await Task.Run(() =>
        {
            if (!_cache.TryGetValue(key, out var value))
                return default;

            return JsonSerializer.Deserialize<T>(value);
        });
    }

    public async Task SetAsync(string key, object value)
    {
        await Task.Run(() =>
        {
            _cache.TryAdd(key, JsonSerializer.Serialize(value));
        });
    }

    public async Task SetAsync<T>(string key, T value)
    {
        await Task.Run(() =>
        {
            _cache.TryAdd(key, JsonSerializer.Serialize(value));
        });
    }
}