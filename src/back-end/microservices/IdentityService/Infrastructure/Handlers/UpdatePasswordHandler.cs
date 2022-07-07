namespace IdentityService.Infrastructure.Handlers;

public sealed class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, IActionResult>
{
    public Task<IActionResult> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}