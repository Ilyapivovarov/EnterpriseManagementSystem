namespace EnterpriseManagementSystem.Cache.Abstractions;

public interface ICacheService
{
    Task SetAsync(string key, string value);

    Task SetAsync(string key, string value, TimeSpan expiry);

    Task SetAsync<TKey, TValue>(TKey key, TValue value, TimeSpan expiry)
        where TKey : notnull
        where TValue : notnull;

    Task<string?> GetStringAsync(string key);   
}