namespace UserService.Infrastructure.Handlers;

public sealed class UpdateUserInfoRequestHandler : HandlerBase<UpdateUserInfoRequest>
{
    private readonly ILogger<UpdateUserInfoRequestHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IUserServices _userServices;

    public UpdateUserInfoRequestHandler(ILogger<UpdateUserInfoRequestHandler> logger, IUserRepository userRepository,
        IUserServices userServices)
    {
        _logger = logger;
        _userRepository = userRepository;
        _userServices = userServices;
    }

    public override async Task<IActionResult> Handle(UpdateUserInfoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userInfo = request.UserInfo;

            var userDbEntity = await _userRepository.GetByGuidAsync(userInfo.Guid);
            if (userDbEntity == null)
                return NotFoud("Not found user");

            _userServices.UpdateUserInfo(userDbEntity, userInfo.FirstName, userInfo.LastName);

            var saveResult = !await _userRepository.UpdateAsync(userDbEntity);
            return saveResult ? Ok() : Error("Error while saving user");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
        
    }
}