using LoggingService.AppContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoggingService.AppContext;

public class LoggingServiceContext : DbContext 
{
    public LoggingServiceContext(DbContextOptions<LoggingServiceContext> options)
        : base(options)
    { }
    
    public DbSet<LogDbEntity> Logs => Set<LogDbEntity>();
}