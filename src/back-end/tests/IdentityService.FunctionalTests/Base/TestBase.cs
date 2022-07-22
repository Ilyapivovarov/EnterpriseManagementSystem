using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityService.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.Base;

public abstract class TestBase
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private IServiceProvider? _service;
    private IServiceScope? _serviceScoped;

    protected TestBase()
    {
        _jsonSerializerOptions = new JsonSerializerOptions
            {PropertyNameCaseInsensitive = true};
    }

    protected abstract string EnvironmentName { get; }

    protected IServiceProvider Service => _service ??= ServiceScope.ServiceProvider;

    protected HttpClient Client { get; set; }

    private IServiceScope ServiceScope => _serviceScoped ??= Server.Services.CreateScope();

    private TestServer Server { get; set; }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Service.GetRequiredService<IdentityDbContext>().Database.EnsureDeletedAsync();
        ServiceScope.Dispose();
    }

    protected async Task RefreshServer()
    {
        Server = CreateTestServer();
        Client = Server.CreateClient();
        await Task.Delay(2000);
    }

    protected StringContent GetStringContent(object obj)
    {
        return new StringContent(
            JsonSerializer.Serialize(obj, _jsonSerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);
    }

    private TestServer CreateTestServer()
    {
        var hostBuilder = new WebHostBuilder()
            .ConfigureAppConfiguration(
                configuration => configuration.AddJsonFile($"appsettings.{EnvironmentName}.json"))
            .UseStartup<Startup>()
            .UseEnvironment(EnvironmentName);

        return new TestServer(hostBuilder);
    }
}