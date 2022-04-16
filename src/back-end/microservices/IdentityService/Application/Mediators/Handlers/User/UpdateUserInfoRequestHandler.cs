namespace IdentityService.Application.Mediators.Handlers.User;

public class UpdateUserInfoRequestHandler : IRequestHandler<Request<UserInfo, UserController>, IActionResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserBlService _userBlService;

    public UpdateUserInfoRequestHandler(IUserRepository userRepository, IUserBlService userBlService)
    {
        _userRepository = userRepository;
        _userBlService = userBlService;
    }

    public async Task<IActionResult> Handle(Request<UserInfo, UserController> request,
        CancellationToken cancellationToken)
    {
        var userInfo = request.Body;
        if (userInfo == null)
            return new BadRequestObjectResult("Body is empty");

        var user = await _userRepository.GetUserByGuidAsync(userInfo.Guid);
        if (user == null)
            return new BadRequestObjectResult($"User is not found");

        _userBlService.ChangeUserInfo(user, userInfo.FirstName, userInfo.LastName, userInfo.Role);
        if (!await _userRepository.UpadteUserAsync(user))
            return new BadRequestObjectResult($"Error while save user data");

        return new OkResult();
    }
    
}