namespace TaskService.Infrastructure.Requests;

public sealed class GetTasksByPageRequest : IRequest<IActionResult>
{
    public GetTasksByPageRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; }

    public int PageSize { get; }

}
