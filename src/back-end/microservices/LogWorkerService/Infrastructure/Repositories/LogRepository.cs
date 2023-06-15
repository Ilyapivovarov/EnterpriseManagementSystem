using LogWorkerService.Application.Repositories;

namespace LogWorkerService.Infrastructure.Repositories;

public sealed class LogRepository : ILogRepository
{
    public Task Save(LogDbEntity logDbEntity)
    {
        throw new NotImplementedException();
    }
}