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

    public static void Update(ref TaskDbEntity taskDbEntity, string? description, string? name,
        TaskStatusDbEntity taskStatusDbEntity, UsersInvolvedInTask usersInvolvedInTask)
    {
        taskDbEntity.Author = usersInvolvedInTask.Author;
        taskDbEntity.Executor = usersInvolvedInTask.Executor;
        taskDbEntity.Description = description;
        taskDbEntity.Inspector = usersInvolvedInTask.Inspector;

        if (name != null)
            taskDbEntity.Name = name;

        var newObservers = taskDbEntity.Observers.UnionBy(usersInvolvedInTask.Observers,
            entity => entity.Guid);
        taskDbEntity.Observers.Clear();
        taskDbEntity.Observers.AddRange(newObservers);
        
        taskDbEntity.Status = taskStatusDbEntity;
    }
}