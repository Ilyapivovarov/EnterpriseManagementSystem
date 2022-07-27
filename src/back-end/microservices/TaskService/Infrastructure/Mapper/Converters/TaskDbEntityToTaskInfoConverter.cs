using AutoMapper;
using EnterpriseManagementSystem.Contracts.Common;

namespace TaskService.Infrastructure.Mapper.Converters;

public sealed class TaskDbEntityToTaskInfoConverter : ITypeConverter<TaskDbEntity, TaskInfo>
{
    public TaskInfo Convert(TaskDbEntity source, TaskInfo destination, ResolutionContext context)
    {
        return new TaskInfo(source.Guid, source.Name, source.Description, source.Status.Name, source.Author.ToDto(),
            source.Executor?.ToDto(),
            source.Inspector?.ToDto(),
            new RecordsCollection<Account>());
    }
}
