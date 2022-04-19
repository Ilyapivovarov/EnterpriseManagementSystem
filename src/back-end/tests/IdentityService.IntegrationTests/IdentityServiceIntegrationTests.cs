using IdentityService.Infrastructure.AppData;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IdentityService.IntegrationTests;

public class IdentityServiceIntegrationTests : TestBase
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestConnectionToDatabase()
    {
        var testServer = GetTestServer();
        var context = testServer.Services.GetRequiredService<ApplicationDbContext>();

        Assert.IsTrue(context.Database.CanConnect());
    }
}