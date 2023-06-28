using EnterpriseManagementSystem.Contracts.Dto.TaskService;

namespace TaskService.Infrastructure.Mapper.Converters;

public sealed class TaskDbEntityToDtoConverter : ITypeConverter<TaskDbEntity, TaskDto>
{
    public TaskDto Convert(TaskDbEntity source, TaskDto destination, ResolutionContext context)
    {
        return new TaskDto(source.Id, source.PublicId, source.Name, source.Description, source.Created,
            source.Author, source.Executor, source.Inspector, source.Status.ToDto());
    }
}