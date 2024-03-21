namespace EnterpriseManagementSystem.Cache.Abstractions;

public interface ICacheService
{
    Task SetAsync<TKey, TValue>(TKey key, TValue value, TimeSpan? expiry = null)
        where TKey : notnull
        where TValue : notnull;

    Task<string?> GetStringAsync(string key);   
}