using EnterpriseManagementSystem.Cache.Abstractions;
using EnterpriseManagementSystem.Cache.Common;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseManagementSystem.Cache;

public static class CacheExtensions
{
    public static void AddCache(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureOptions<CacheConfigureSetUp>();
        serviceCollection.AddTransient<ICacheServiceProvider, CacheServiceProvider>();
        serviceCollection.AddSingleton<ICacheService>(x
            => x.GetRequiredService<ICacheServiceProvider>().UseCache());
    }
}