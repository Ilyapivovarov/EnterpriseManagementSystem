namespace TaskService.Application.Repositories;

public interface ITaskStatusRepository
{
    #region Write methods

    public Task<bool> SaveAsync(TaskStatusDbEntity taskStatusDbEntity);

    #endregion

    #region Read methods

    public Task<TaskStatusDbEntity?> GetById(int id);

    public Task<TaskStatusDbEntity?> GetByGuid(Guid guid);

    public Task<TaskStatusDbEntity> GetDefaultTaskStatus();

    public Task<TaskStatusDbEntity?> GetByName(string statusName);

    public Task<TaskStatusDbEntity[]?> GetAllStatuses();

    #endregion
}
