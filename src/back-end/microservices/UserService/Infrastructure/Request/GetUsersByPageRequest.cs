namespace UserService.Infrastructure.Request;

public sealed class GetUsersByPageRequest : IRequest<IActionResult>
{
    public GetUsersByPageRequest(int pageNumber)
    {
        PageNumber = pageNumber;
    }

    public int PageNumber { get; }
}