using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using IdentityService.Application.Models;
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
        User = GetUser();
        Server = GetTestServer();
        JsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }
    
    protected UserDbEntity User { get; }
    
    protected  TestServer Server { get; }
    
    protected  JsonSerializerOptions JsonSerializerOptions { get; }
    
    protected string? AccessToken { get; set; }

    [OneTimeTearDown]
    public void RemoveDatabase()
    {
        var context = Server.Services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted();
    }

    private TestServer GetTestServer()
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
            context.Users.Add(User);
            context.SaveChanges();
        }
        
        return test;
    }
    
    private UserDbEntity GetUser()
    {
        return new UserDbEntity()
        {
            Email = "admin@admin.com",
            Password = "admin",
            FirstName = "Admin",
            LastName = "Admin",
            Role = UserRole.Admin
        };
    }
}