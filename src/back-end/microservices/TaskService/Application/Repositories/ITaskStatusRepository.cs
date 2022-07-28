namespace TaskService.Application.Repositories;

public interface ITaskStatusRepository
{
    public Task<bool> SaveAsync(TaskStatusDbEntity taskStatusDbEntity);

    public Task<TaskStatusDbEntity?> GetByGuid(Guid guid);

    public Task<TaskStatusDbEntity> GetDefaultTaskStatus();

    public Task<TaskStatusDbEntity?> GetByName(string statusName);

    public Task<TaskStatusDbEntity[]?> GetAllStatuses();
}
