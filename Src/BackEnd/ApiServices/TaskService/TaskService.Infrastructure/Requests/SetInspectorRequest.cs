namespace TaskService.Infrastructure.Requests;

public sealed class SetInspectorRequest : IRequest<IActionResult>
{
    public SetInspectorRequest(int taskId, Guid? inspectorId)
    {
        TaskId = taskId;
        InspectorId = inspectorId;
    }
    
    public int TaskId { get; }

    public Guid? InspectorId { get; }
}