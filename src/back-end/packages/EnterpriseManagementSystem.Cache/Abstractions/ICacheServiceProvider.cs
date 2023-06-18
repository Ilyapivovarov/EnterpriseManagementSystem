﻿using EnterpriseManagementSystem.Cache.CacheServices;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace EnterpriseManagementSystem.Cache.Abstractions;

public interface ICacheServiceProvider
{
    public ICacheService UseCache();
}

public class CacheServiceProvider : ICacheServiceProvider
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly CacheServiceConfiguration _options;

    public CacheServiceProvider(IOptions<CacheServiceConfiguration> options, IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
        _options = options.Value;
    }

    public ICacheService UseCache()
    {
        return _hostEnvironment.IsStaging()
            ? new TestCacheService()
            : new RedisCacheService(ConnectionMultiplexer.Connect(_options.ConnectionString));
    }
}