using IdentityService.Application.Services;
using IdentityService.IntegrationTests.Base;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using StackExchange.Redis;

namespace IdentityService.IntegrationTests;

public sealed class IdentityServiceIntegrationTests : TestBase
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void TestDevConnectionToDb()
    {
        // var testServer = GetTestServer();
        // var context = testServer.Services.GetRequiredService<IdentityDbContext>();

        Assert.Warn("Not implement");
    }

    [Test]
    public void TestConnectionToRedis()
    {
        var ts = GetTestServer();

        using var serivces = ts.Services.CreateScope();
        var cm = serivces.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
        
        Assert.That(cm.IsConnected, Is.True);
    }
}