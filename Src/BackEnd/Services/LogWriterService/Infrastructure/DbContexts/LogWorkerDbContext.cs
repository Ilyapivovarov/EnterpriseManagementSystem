

using LogWriterService.Application.DbContexts;
using LogWriterService.Core.DbEntities;

namespace LogWriterService.Infrastructure.DbContexts;

public class LogWorkerDbContext : DbContext, ILogWorkerDbContext
{
    public LogWorkerDbContext(DbContextOptions<LogWorkerDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<LogDbEntity> Logs => Set<LogDbEntity>();
    public async Task MigrateAsync()
    {
        await Database.MigrateAsync();
    }
}