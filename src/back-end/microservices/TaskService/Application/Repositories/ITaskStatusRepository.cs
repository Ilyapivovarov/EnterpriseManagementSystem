namespace TaskService.Application.Repositories;

public interface ITaskStatusRepository
{
    public Task<TaskStatusDbEntity> GetByGuid(Guid guid);

    public Task<TaskStatusDbEntity> GetByName(string statusName);
}