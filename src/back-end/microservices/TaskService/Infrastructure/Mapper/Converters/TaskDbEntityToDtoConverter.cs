namespace TaskService.Infrastructure.Mapper.Converters;

public sealed class TaskDbEntityToDtoConverter : ITypeConverter<TaskDbEntity, TaskDto>
{
    public TaskDto Convert(TaskDbEntity source, TaskDto destination, ResolutionContext context)
    {
        return new TaskDto(source.Id, source.Guid, source.Name, source.Description, source.Created,
            source.Author.ToDto(), source.Executor?.ToDto(), source.Inspector?.ToDto(), source.Status.ToDto());
    }
}