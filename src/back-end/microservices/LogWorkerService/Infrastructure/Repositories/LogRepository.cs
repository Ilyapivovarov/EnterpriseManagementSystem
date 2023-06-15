using LogWorkerService.Application.Repositories;

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