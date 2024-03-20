namespace IdentityService.Infrastructure.Requests;

public sealed class UpdatePasswordRequest : IRequest<IActionResult>
{
    public UpdatePasswordRequest(UpdatePasswordInfo newPasswordInfo)
    {
        NewPasswordInfo = newPasswordInfo;
    }

    public UpdatePasswordInfo NewPasswordInfo { get; }
}