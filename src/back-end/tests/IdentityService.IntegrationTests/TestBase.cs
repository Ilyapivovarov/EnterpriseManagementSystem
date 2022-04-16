using System;
using System.IO;
using System.Reflection;
using IdentityService.Infrastructure.AppData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace IdentityService.IntegrationTests;

public class TestBase
{
    protected TestServer GetTestServer()
    {
        var path = Assembly.GetExecutingAssembly().Location;

        var hostBuilder = new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(path) ?? throw new ArgumentNullException(path))
            .ConfigureAppConfiguration(configuration =>
            {
                configuration.AddJsonFile("appsettings.json", false)
                    .AddEnvironmentVariables();
            }).UseStartup<Startup>();

        var testServer = new TestServer(hostBuilder);
        
        return testServer;
    }
}