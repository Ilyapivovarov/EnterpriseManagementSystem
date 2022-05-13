namespace TaskService.Application.Repositories;

public interface ITaskRepository
{
    /// <summary>
    ///     Getting entity by guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public Task<TaskDbEntity?> GetTaskByGuidAsync(Guid guid);

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="taskDbEntity"></param>
    /// <returns></returns>
    public Task<bool> UpdateTaskAsync(TaskDbEntity taskDbEntity);

    /// <summary>
    ///     Save entity
    /// </summary>
    /// <param name="taskDbEntity"></param>
    /// <returns></returns>
    public Task<bool> SaveTaskAsync(TaskDbEntity taskDbEntity);
}