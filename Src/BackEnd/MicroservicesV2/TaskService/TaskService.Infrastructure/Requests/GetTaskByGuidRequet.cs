namespace TaskService.Infrastructure.Requests;

public sealed class GetTaskByGuidRequest : IRequest<IActionResult>
{
    public GetTaskByGuidRequest(Guid taskGuid)
    {
        TaskGuid = taskGuid;
    }

    public Guid TaskGuid { get; }
}