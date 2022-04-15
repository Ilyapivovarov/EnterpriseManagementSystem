using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using IdentityService.Infrastructure.AppData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IdentityService.FunctionalTests;

public class TestBase
{
    public TestBase()
    {
        Server = GetTestServer();
    }

    protected TestServer Server { get; }

    [OneTimeTearDown]
    public void RemoveDatabase()
    {
        var context = Server.Services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted();
    }

    private static TestServer GetTestServer()
    {
        var path = Assembly.GetExecutingAssembly().Location;

        var hostBuilder = new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(path) ?? throw new ArgumentNullException(nameof(path)))
            .ConfigureAppConfiguration(configuration =>
            {
                configuration.AddJsonFile("appsettings.json", false)
                    .AddEnvironmentVariables();
            }).UseStartup<Startup>();

        return new TestServer(hostBuilder);
    }
}