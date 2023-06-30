namespace IdentityService.Infrastructure.Requests;

public sealed class SignOutRequest : IRequest<IActionResult>
{
    public SignOutRequest(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; }
}