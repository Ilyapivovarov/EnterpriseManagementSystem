namespace UserService.Infrastructure.Request;

public sealed class GetEmployeesByPageRequest : IRequest<IActionResult>
{
    public GetEmployeesByPageRequest(int pageNumber)
    {
        PageNumber = pageNumber;
    }

    public int PageNumber { get; }
}