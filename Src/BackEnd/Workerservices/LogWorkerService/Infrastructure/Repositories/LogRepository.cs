using LogWorkerService.Application.DbContexts;
using LogWorkerService.Application.Repositories;
using LogWorkerService.Core.DbEntities;

namespace LogWorkerService.Infrastructure.Repositories;

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