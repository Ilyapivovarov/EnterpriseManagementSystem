using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseManagementSystem.Cache.Tests;

[TestFixture]
public class UnitTests
{
    [Test]
    public void CacheExtensions_AddCache_Test()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCache();
        
        Assert.Pass();
    }

    [Test]
    public void CacheConfigureSetUp_Test()
    {
        const string testValue = "Value1";
        var inMemorySettings = new Dictionary<string, string>
        {
            { "Cache:ConnectionString", testValue },
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        var options = new Mock<CacheServiceConfiguration>().Object;
        new CacheConfigureSetUp(configuration)
            .Configure(options);
        
        Assert.That(options.ConnectionString, Is.EqualTo(testValue));
    }
}