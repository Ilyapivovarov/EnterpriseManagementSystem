namespace TaskService.Core.DbEntities.Builders;

public static class TaskDbEntityBuilder
{
    public static TaskDbEntity CreateNew(string description, string name,
        TaskStatusDbEntity taskStatusDbEntity, UsersInvolvedInTask usersInvolvedInTask)
    {
        return new TaskDbEntity
        {
            Author = usersInvolvedInTask.Author,
            Executor = usersInvolvedInTask.Executor,
            Description = description,
            Inspector = usersInvolvedInTask.Inspector,
            Name = name,
            Observers = usersInvolvedInTask.Observers,
            Status = taskStatusDbEntity
        };
    }
}