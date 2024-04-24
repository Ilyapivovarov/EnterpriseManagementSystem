namespace EnterpriseManagementSystem.Cache.Common;

public class CacheConfigureSetUp : IConfigureOptions<CacheServiceConfiguration>
{
    private const string CacheSectionName = "Cache";

    private readonly IConfiguration _configuration;

    public CacheConfigureSetUp(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(CacheServiceConfiguration options)
    {
        _configuration.GetRequiredSection(CacheSectionName).Bind(options);
    }
}