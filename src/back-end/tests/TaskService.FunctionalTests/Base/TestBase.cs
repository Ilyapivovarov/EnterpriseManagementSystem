using System;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TaskService.FunctionalTests.Base;

public abstract class TestBase
{
    protected TestBase()
    {
        Server = CreateTestServer();
        JsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

    protected TestServer Server { get; }

    protected JsonSerializerOptions JsonSerializerOptions { get; }

    protected string? AccessToken { get; set; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost")
        };
        var signInContent =
            new StringContent(
                JsonSerializer.Serialize(new SignIn("admin@admin.com", "admin"), JsonSerializerOptions),
                Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("auth/sign-in", signInContent);

        var sessionDraft = await response.Content.ReadAsStringAsync();
        var sessionDto = JsonSerializer.Deserialize<Session>(sessionDraft, JsonSerializerOptions);

        AccessToken = sessionDto?.AccessToken;
    }

    private static TestServer CreateTestServer()
    {
        var path = Assembly.GetExecutingAssembly().Location;

        var hostBuilder = new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(path) ?? throw new ArgumentNullException(path))
            .ConfigureAppConfiguration(configuration =>
            {
                configuration.AddJsonFile("appsettings.Development.json", false)
                    .AddEnvironmentVariables("Development");
            }).UseStartup<Startup>();

        return new TestServer(hostBuilder);
    }
}