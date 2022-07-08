using UserService.Infrastructure.Handlers.Base;

namespace UserService.Infrastructure.Handlers;

public sealed class UpdateUserInfoRequestHandler : HandlerBase<UpdateUserInfoRequest>
{
    public override Task<IActionResult> Handle(UpdateUserInfoRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}