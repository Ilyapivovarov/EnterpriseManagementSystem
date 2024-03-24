using Microsoft.Extensions.Options;

namespace LoggingService.MongoDbStorage.Options;

public class MongoDbStorageOptionSetUp : IConfigureOptions<MongoDbStorageOption>
{
    private const string MongoDbStorageSectionName = "Mongo";
    
    private readonly IConfiguration _configuration;

    public MongoDbStorageOptionSetUp(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(MongoDbStorageOption options)
    {
        _configuration.GetRequiredSection(MongoDbStorageSectionName).Bind(options);
    }
}