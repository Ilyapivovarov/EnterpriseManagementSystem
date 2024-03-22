using System.ComponentModel.DataAnnotations.Schema;

namespace LoggingService.AppContext.Entities;

public class LogDbEntity
{
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Uid { get; protected set; } = Guid.NewGuid();

    public LogLevel Level { get; set; }

    public required string AppName { get; set; }
    
    public required string Message { get; set; }

    public string? Exception { get; set; }
}