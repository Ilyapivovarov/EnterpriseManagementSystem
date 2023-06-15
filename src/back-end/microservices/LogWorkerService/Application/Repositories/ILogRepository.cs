namespace LogWorkerService.Application.Repositories;

public interface ILogRepository
{
    public Task Save(LogDbEntity logDbEntity);
}