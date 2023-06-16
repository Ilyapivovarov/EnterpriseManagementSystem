namespace LogWorkerService.Application.DbContexts;

public interface ILogWorkerDbContext
{
    DbSet<LogDbEntity> Logs { get; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    public Task MigrateAsync();
}