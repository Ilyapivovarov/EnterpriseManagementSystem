namespace EnterpriseManagementSystem.Logging.Options;

public class DbLoggerOptions
{
    public required string AppName { get; set; }

    public required Dictionary<string, LogLevel> LogLevel { get; set; }
}