using System;
using IdentityService.Application;
using IdentityService.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace IdentityService.UnitTests.Base;

public abstract class TestBase : IDisposable
{
    private const string EnvironmentName = "Testing";

    protected TestBase()
    {
        Environment = Mock.Of<IWebHostEnvironment>();
        Environment.EnvironmentName = EnvironmentName;

        Configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.{EnvironmentName}.json")
            .Build();

        Container = InitContainer();
    }

    private IWebHostEnvironment Environment { get; }

    private IConfiguration Configuration { get; }

    protected ServiceProvider Container { get; }

    private ServiceProvider InitContainer()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddApplication(Configuration, Environment);
        serviceCollection.AddInfrastructure(Configuration, Environment);

        return serviceCollection.BuildServiceProvider();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
