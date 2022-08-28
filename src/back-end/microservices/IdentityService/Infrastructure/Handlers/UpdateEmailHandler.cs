namespace IdentityService.Infrastructure.Handlers;

public sealed class UpdateEmailHandler : IRequestHandler<UpdateEmailRequest, IActionResult>
{
    private readonly ILogger<UpdateEmailHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public UpdateEmailHandler(ILogger<UpdateEmailHandler> logger, IUserRepository userRepository,
        IUserService userService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<IActionResult> Handle(UpdateEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var updateEmailInfo = request.UpdateEmailInfo;

            var userDbEntity = await _userRepository.GetUserByEmailAsync(EmailAddress.TryParse(updateEmailInfo.Email));
            if (userDbEntity == null)
                return new NotFoundObjectResult("Not found user with this email");

            var serviceResult = _userService.ChangeEmail(userDbEntity, EmailAddress.TryParse(updateEmailInfo.NewEmail));
            if (serviceResult.HasError)
                return new BadRequestObjectResult(serviceResult.Error);

            var saveResult = await _userRepository.UpadteUserAsync(userDbEntity);
            if (!saveResult)
                return new BadRequestObjectResult("Error while update user");

            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}