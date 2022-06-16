using IdentityService.Infrastructure.AppData;
using IdentityService.IntegrationTests.Base;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IdentityService.IntegrationTests;

public sealed class IdentityServiceIntegrationTests : TestBase
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestDevConnectionToDb()
    {
        var testServer = GetTestServer();
        var context = testServer.Services.GetRequiredService<IdentityDbContext>();

        Assert.IsTrue(context.Database.CanConnect());
    }
}