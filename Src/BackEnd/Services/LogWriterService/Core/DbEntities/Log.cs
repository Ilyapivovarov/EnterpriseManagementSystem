namespace LogWriterService.Core.DbEntities;

public class LogDbEntity
{
    public long Id { get; set; }

    public required string AppName { get; set; }
    
    public required DateTime DateTime { get; init; }
    
    public required string Log { get; init; }

    public required string Method { get;  init; }

    public required string Message { get; init; }
}