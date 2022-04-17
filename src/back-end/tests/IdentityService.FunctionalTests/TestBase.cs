using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityService.Application.Models;
using IdentityService.Infrastructure.AppData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace IdentityService.FunctionalTests;

public class TestBase
{
    public TestBase()
    {
        EmptyServer = GetEmptyTestServer();
    }

    protected TestServer EmptyServer { get; }

    [OneTimeTearDown]
    public void RemoveDatabase()
    {
        var context = EmptyServer.Services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted();
    }

    private static TestServer GetEmptyTestServer()
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

    protected static TestServer GetTestServer()
    {
        var path = Assembly.GetExecutingAssembly().Location;

        var hostBuilder = new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(path) ?? throw new ArgumentNullException(nameof(path)))
            .ConfigureAppConfiguration(configuration =>
            {
                configuration.AddJsonFile("appsettings.json", false)
                    .AddEnvironmentVariables();
            }).UseStartup<Startup>();
        
        var test = new TestServer(hostBuilder);
        var context = test.Services.GetRequiredService<ApplicationDbContext>();
        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Email = "admin@admin.com",
                Password = "admin",
                FirstName = "Admin",
                LastName = "Admin",
                Role = UserRole.Admin
            });
            
            
            context.SaveChanges();
        }


        return test;
    }
}