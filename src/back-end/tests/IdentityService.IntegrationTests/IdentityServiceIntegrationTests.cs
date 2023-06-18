using IdentityService.IntegrationTests.Base;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace IdentityService.IntegrationTests;

public sealed class IdentityServiceIntegrationTests : TestBase
{
    protected override string Environment => "Development";

    [Test]
    public void TestDevConnectionToDb()
    {
        var connectionString = new SqlConnectionStringBuilder(Configuration.GetConnectionString("RelationalDb"))
        {
            InitialCatalog = "master"
        };

        using var sqlConnections = new SqlConnection(connectionString.ToString());
        Assert.DoesNotThrow(sqlConnections.Open);
    }

    [Test]
    public void TestConnectionToRedis()
    {
        // var cm = ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis"));
        // Assert.That(cm.IsConnected, Is.True);
    }
}
