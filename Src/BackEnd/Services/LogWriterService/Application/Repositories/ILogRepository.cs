using LogWriterService.Core.DbEntities;

namespace LogWriterService.Application.Repositories;

public interface ILogRepository
{
    public Task Save(LogDbEntity logDbEntity);
}