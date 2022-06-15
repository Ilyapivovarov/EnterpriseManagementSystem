using AutoMapper;

namespace TaskService.Infrastructure.Mapper.Converters;

public sealed class TaskDbEntityToTaskInfoConverter : ITypeConverter<TaskDbEntity, TaskInfo>
{
    public TaskInfo Convert(TaskDbEntity source, TaskInfo destination, ResolutionContext context)
    {
        return new TaskInfo(source.Guid, source.Name, source.Description, source.Status.Name, source.Author.ToDto(),
            source.Executor?.ToDto(),
            source.Inspector?.ToDto(),
            source.Observers?.ToDto());
    }
}

public sealed class UserDbEntityToAccountConverter : ITypeConverter<UserDbEntity, Account>
{
    public Account Convert(UserDbEntity source, Account destination, ResolutionContext context)
    {
        return new Account(source.Guid, source.EmailAddress, source.FirstName, source.LastName, source.Role);
    }
}