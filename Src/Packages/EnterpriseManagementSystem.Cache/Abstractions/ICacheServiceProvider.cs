namespace EnterpriseManagementSystem.Cache.Abstractions;

public interface ICacheServiceProvider
{
    public ICacheService UseCache();
}
