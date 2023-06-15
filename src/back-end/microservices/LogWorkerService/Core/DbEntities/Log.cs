namespace LogWorkerService.Core.DbEntities;

public class LogDbEntity
{
    public required DateTime DateTime { get; init; }
    
    public required LogLevel Log { get; init; }

    public required string Method { get;  init; }

    public required string Message { get; init; }
}