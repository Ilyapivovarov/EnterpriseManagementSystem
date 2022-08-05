namespace TaskService.Application.Repositories;

public interface ITaskRepository
{
    #region Read methods

    public Task<int> GetTasksCount();

    /// <summary>
    ///     Getting entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<TaskDbEntity?> GetTaskByIdAsync(int id);

    /// <summary>
    ///     Getting entity by guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public Task<TaskDbEntity?> GetTaskByGuidAsync(Guid guid);

    /// <summary>
    ///
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public Task<TaskDbEntity[]?> GetTasksByPage(int pageNumber, int pageSize);

    #endregion

    #region Write methods

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="taskDbEntity"></param>
    /// <returns></returns>
    public Task<bool> UpdateAsync(TaskDbEntity taskDbEntity);

    /// <summary>
    ///     Save entity
    /// </summary>
    /// <param name="taskDbEntity"></param>
    /// <returns></returns>
    public Task<bool> SaveTaskAsync(TaskDbEntity taskDbEntity);

    #endregion
}
