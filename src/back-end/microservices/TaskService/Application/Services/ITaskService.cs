namespace TaskService.Application.Services;

public interface ITaskService
{
    /// <summary>
    ///     Get users involved in task
    /// </summary>
    /// <param name="authorGuid"></param>
    /// <param name="executorGuid"></param>
    /// <param name="inspectorGuid"></param>
    /// <param name="observerGuids"></param>
    /// <returns></returns>
    public Task<UsersInvolvedInTask> GetUsersInvolvedInTask(Guid authorGuid, Guid? executorGuid = null,
        Guid? inspectorGuid = null, ICollection<Guid>? observerGuids = null);

    /// <summary>
    ///     Geting or creating task with name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<ServiceResult<TaskStatusDbEntity>> GetOrCreateTaskByName(string name);

    /// <summary>
    /// Update task status
    /// </summary>
    /// <param name="taskStatusDbEntity"></param>
    /// <param name="taskDbEntity"></param>
    /// <returns></returns>
    public ServiceResult<TaskDbEntity> UpdateTaskStatus(TaskStatusDbEntity taskStatusDbEntity,
        TaskDbEntity taskDbEntity);
}
