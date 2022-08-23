namespace TaskService.Infrastructure.Requests;

public sealed class SetInspectorRequest : IRequest<IActionResult>
{
    public SetInspectorRequest(int taskId, int inspectorId)
    {
        TaskId = taskId;
        InspectorId = inspectorId;
    }
    
    public int TaskId { get; }

    public int InspectorId { get; }
}