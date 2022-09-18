namespace IdentityService.Infrastructure.Repositories.Base;

public abstract class CacheRepositoryBase
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<CacheRepositoryBase> _logger;

    protected CacheRepositoryBase(ICacheService cacheService, ILogger<CacheRepositoryBase> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    protected async Task<T?> LoadDataAsync<T>(string key)
    {
        try
        {
           return await _cacheService.TryGetAsync<T>(key);
           
        }
        catch (Exception e)
        {
           _logger.LogError(e.Message);
           return default;
        }
    }
    
    protected async Task<bool> WriteAsync<T>(string key, T value)
    {
        try
        {
            await _cacheService.SetAsync(key, value);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
    
}