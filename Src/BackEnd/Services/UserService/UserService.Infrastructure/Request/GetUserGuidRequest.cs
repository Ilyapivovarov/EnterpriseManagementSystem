namespace UserService.Infrastructure.Request;

public sealed class GetUserGuidRequest : IRequest<IActionResult>
{
    public GetUserGuidRequest(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; }
}