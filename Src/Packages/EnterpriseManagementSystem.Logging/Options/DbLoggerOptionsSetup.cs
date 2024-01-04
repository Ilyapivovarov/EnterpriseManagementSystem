namespace EnterpriseManagementSystem.Logging.Options;

public class DbLoggerOptionsSetup : IConfigureOptions<DbLoggerOptions>
{
    private const string DbLoggerSectionName = "Logging:DbLogger";
    
    private readonly IConfiguration _configuration;

    public DbLoggerOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(DbLoggerOptions options)
    {
        _configuration.GetSection(DbLoggerSectionName)
            .Bind(options);
    }
}