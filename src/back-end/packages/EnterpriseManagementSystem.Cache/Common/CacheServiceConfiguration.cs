namespace EnterpriseManagementSystem.Cache.Common;

public class CacheServiceConfiguration
{
    public required string ConnectionString { get; set; }

    public CacheType CacheType { get; set; }
}

public enum CacheType
{
    InMemory,
    Redis,
} 