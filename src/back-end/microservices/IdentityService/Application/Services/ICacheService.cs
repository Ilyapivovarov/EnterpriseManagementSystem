namespace IdentityService.Application.Services;

public interface ICacheService
{
    Task<string> GetAsync(string key);

    Task SetAsync(string key, string value);
}