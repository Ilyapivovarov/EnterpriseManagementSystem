namespace IdentityService.Application.Mediators.Handlers.User;

public sealed class UpdateUserInfoRequestHandler : IRequestHandler<UserControllerRequest<UserInfo>, IActionResult>
{
    private readonly ILogger<UpdateUserInfoRequestHandler> _logger;
    private readonly IUserBlService _userBlService;
    private readonly IUserRepository _userRepository;

    public UpdateUserInfoRequestHandler(ILogger<UpdateUserInfoRequestHandler> logger, IUserRepository userRepository,
        IUserBlService userBlService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _userBlService = userBlService;
    }

    public async Task<IActionResult> Handle(UserControllerRequest<UserInfo> request,
        CancellationToken cancellationToken)
    {
        try
        {
            var userInfo = request.Body;
            if (userInfo == null)
                return new BadRequestObjectResult("Body is empty");

            var user = await _userRepository.GetUserByGuidAsync(userInfo.Guid);
            if (user == null)
                return new BadRequestObjectResult("User is not found");

            _userBlService.ChangeUserInfo(user, userInfo.FirstName, userInfo.LastName, userInfo.Role);
            if (!await _userRepository.UpadteUserAsync(user))
                return new BadRequestObjectResult("Error while save user data");

            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}