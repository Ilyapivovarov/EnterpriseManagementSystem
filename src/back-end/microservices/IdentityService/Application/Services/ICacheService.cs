namespace IdentityService.Application.Services;

public interface ICacheService
{
    Task<object?> GetAsync(string key);

    Task<T?> TryGetAsync<T>(string key);

    Task SetAsync(string key, object value);
    
    Task SetAsync<T>(string key, T value);
}