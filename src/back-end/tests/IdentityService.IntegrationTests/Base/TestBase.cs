using Microsoft.Extensions.Configuration;

namespace IdentityService.IntegrationTests.Base;

public abstract class TestBase
{
    protected abstract string Environment { get; }


    protected IConfiguration Configuration => new ConfigurationBuilder()
        .AddJsonFile($"appsettings.{Environment}.json")
        .Build();
}
