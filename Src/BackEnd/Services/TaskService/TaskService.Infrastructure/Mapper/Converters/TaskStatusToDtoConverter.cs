namespace TaskService.Infrastructure.Mapper.Converters;

public sealed class TaskStatusToDtoConverter : ITypeConverter<TaskStatusDbEntity, TaskStatusDto>
{
    public TaskStatusDto Convert(TaskStatusDbEntity source, TaskStatusDto destination, ResolutionContext context)
    {
        return new TaskStatusDto(source.Id, source.PublicId, source.Name);
    }
}