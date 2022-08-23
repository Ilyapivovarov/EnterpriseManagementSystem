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
    
    /// <summary>
    /// Update executor
    /// </summary>
    /// <param name="userDbEntity"></param>
    /// <param name="taskDbEntity"></param>
    /// <returns></returns>
    public ServiceResult<TaskDbEntity> SetExecutor(UserDbEntity userDbEntity,
        TaskDbEntity taskDbEntity);

    public ServiceResult<TaskDbEntity> SetInspector(UserDbEntity inspector, TaskDbEntity task);
    
    /// <summary>
    /// Update task and name for task
    /// </summary>
    /// <param name="taskDbEntity"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public ServiceResult<TaskDbEntity> UpdateTask(TaskDbEntity taskDbEntity, string name, string? description);

    /// <summary>
    /// Create task
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="author"></param>
    /// <param name="status"></param>
    /// <param name="executor"></param>
    /// <param name="inspector"></param>
    /// <returns></returns>
    public ServiceResult<TaskDbEntity> CreateTask(string name, string? description, UserDbEntity author,
        TaskStatusDbEntity status, UserDbEntity? executor, UserDbEntity? inspector);
}
