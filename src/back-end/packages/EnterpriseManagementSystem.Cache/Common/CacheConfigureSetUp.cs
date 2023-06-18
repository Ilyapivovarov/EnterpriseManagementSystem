using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EnterpriseManagementSystem.Cache.Common;

public class CacheConfigureSetUp : IConfigureOptions<CacheServiceConfiguration>
{
    private readonly IConfiguration _configuration;

    public CacheConfigureSetUp(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(CacheServiceConfiguration options)
    {
        _configuration.GetRequiredSection("Cache").Bind(options);;
    }
}