using IdentityService.IntegrationTests.Base;
using NUnit.Framework;

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
}