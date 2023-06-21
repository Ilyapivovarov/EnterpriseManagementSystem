using Microsoft.Extensions.Logging;

namespace EnterpriseManagementSystem.Logging.Options;

public class DbLoggerOptions
{
    public required string AppName { get; set; }

    public required LogLevel LogLevel { get; set; }
}