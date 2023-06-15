namespace LogWorkerService.Core.DbEntities;

public class LogDbEntity
{
    public long Id { get; set; }
    
    public required DateTime DateTime { get; init; }
    
    public required LogLevel Log { get; init; }

    public required string Method { get;  init; }

    public required string Message { get; init; }
}