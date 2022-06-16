namespace IdentityService.Infrastructure.Requests;

public sealed class GetUserByGuidRequest : IRequest<IActionResult>
{
    public GetUserByGuidRequest(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; }
}