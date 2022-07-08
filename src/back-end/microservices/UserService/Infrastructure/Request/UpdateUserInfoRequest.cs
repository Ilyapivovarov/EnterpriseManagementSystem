namespace UserService.Infrastructure.Request;

public sealed class UpdateUserInfoRequest : IRequest<IActionResult>
{
    public UpdateUserInfoRequest(UserInfo userInfo)
    {
        UserInfo = userInfo;
    }

    public UserInfo UserInfo { get; }
}