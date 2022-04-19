using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.Application.Models;
using IdentityService.Infrastructure.AppData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.Base;

public class TestBase
{
    private UserDbEntity? _userDbEntity;
    
    public TestBase()
    {
        Server = CreateTestServer();
        JsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

    protected UserDbEntity User => _userDbEntity ??= GetUserFromDb();
    
    protected TestServer Server { get; }

    protected JsonSerializerOptions JsonSerializerOptions { get; }

    protected string? AccessToken { get; set; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var client = Server.CreateClient();
        var signInContent = new StringContent(JsonSerializer.Serialize(new SignIn(User.EmailAddress.Email, User.Password), JsonSerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("auth/sign-in", signInContent);
        
        var sessionDraft = await response.Content.ReadAsStringAsync();
        var sessionDto = JsonSerializer.Deserialize<Session>(sessionDraft, JsonSerializerOptions);

        AccessToken = sessionDto?.AccessToken;
    }
    
    [OneTimeTearDown]
    public void RemoveDatabase()
    {
        var context = Server.Services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted();
    }
    
    private UserDbEntity GetUserFromDb()
    {
        var context = Server.Services.GetRequiredService<ApplicationDbContext>();
        return context.Users.First();
    }
    
    private static TestServer CreateTestServer()
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
            context.Users.Add(new UserDbEntity()
            {
                EmailAddress = new EmailAddressDbEntity
                {
                    Email = "admin@admin.com",
                    IsVerified = false
                },
                Password = "admin",
                FirstName = "Admin",
                LastName = "Admin",
                Role = new UserRoleDbEntity()
                {
                    Name = "Admin"
                }
            });
            context.SaveChanges();
        }

        return test;
    }
}