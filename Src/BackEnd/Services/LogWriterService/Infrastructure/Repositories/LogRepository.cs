using LogWriterService.Application.DbContexts;
using LogWriterService.Application.Repositories;
using LogWriterService.Core.DbEntities;

namespace LogWriterService.Infrastructure.Repositories;

public sealed class LogRepository : ILogRepository
{
    private readonly ILogWorkerDbContext _logWorkerDbContext;

    public LogRepository(ILogWorkerDbContext logWorkerDbContext)
    {
        _logWorkerDbContext = logWorkerDbContext;
    }

    public async Task Save(LogDbEntity logDbEntity)
    {
        _logWorkerDbContext.Logs.Add(logDbEntity);
        await _logWorkerDbContext.SaveChangesAsync();
    }
}