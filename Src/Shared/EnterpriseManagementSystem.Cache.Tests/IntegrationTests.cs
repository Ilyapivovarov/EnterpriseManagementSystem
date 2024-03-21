﻿using EnterpriseManagementSystem.Cache.Abstractions;
using EnterpriseManagementSystem.Cache.CacheServices;
using EnterpriseManagementSystem.Cache.Common;
using Microsoft.Extensions.Options;
using Moq;

namespace EnterpriseManagementSystem.Cache.Tests;

public class IntegrationTests
{
    private const string TestKey = "IntegrationTestsKey";
    private const string TestValue = "IntegrationTestsValue";

    private static readonly CacheServiceConfiguration CacheServiceConfiguration = new()
    {
        ConnectionString = "localhost:6379"
    };

    public required ICacheService CacheService { get; set; }

    [Test]
    [SetUp]
    public void Connection_Test()
    {
        var options = new Mock<IOptions<CacheServiceConfiguration>>();
        options.Setup(x => x.Value)
            .Returns(() => CacheServiceConfiguration);

        var cacheProvider = new CacheServiceProvider(options.Object);

        CacheService = cacheProvider.UseCache();
    }

    [Test]
    [Order(1)]
    public async Task SetAsync_Test()
    {
        await CacheService.SetAsync(TestKey, TestValue);

        Assert.Pass();
    }

    [Test]
    [Order(2)]
    public async Task GetStringAsync_Test()
    {
        var value = await CacheService.GetStringAsync(TestKey);

        Assert.That(value, Is.EqualTo(TestValue));
    }
}