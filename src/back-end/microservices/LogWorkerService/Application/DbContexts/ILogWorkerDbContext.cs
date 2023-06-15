namespace LogWorkerService.Application.DbContexts;

public interface ILogWorkerDbContext
{
    DbSet<LogDbEntity> Logs { get; }
}