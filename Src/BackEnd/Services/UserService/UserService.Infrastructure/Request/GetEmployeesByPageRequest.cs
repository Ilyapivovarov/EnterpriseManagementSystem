namespace UserService.Infrastructure.Request;

public sealed class GetEmployeesByPageRequest : IRequest<IActionResult>
{
    public GetEmployeesByPageRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; }

    public int PageSize { get; }
}