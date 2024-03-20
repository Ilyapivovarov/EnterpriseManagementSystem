namespace TaskService.Infrastructure.Requests;

public sealed class GetUsersByPageRequest : IRequest<IActionResult>
{
    public GetUsersByPageRequest(int pageNumber, int count)
    {
        PageNumber = pageNumber;
        Count = count;
    }

    public int PageNumber { get; }

    public int Count { get; }
}
