using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseManagementSystem.Cache.Tests;

[TestFixture]
public class UnitTests
{
    private const string TestValue = "Value1";
    
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
        var inMemorySettings = new Dictionary<string, string>
        {
            { "Cache:ConnectionString", TestValue },
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        var options = new Mock<CacheServiceConfiguration>().Object;
        new CacheConfigureSetUp(configuration)
            .Configure(options);
        
        Assert.That(options.ConnectionString, Is.EqualTo(TestValue));
    }
}