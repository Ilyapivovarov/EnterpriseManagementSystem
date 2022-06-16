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
}