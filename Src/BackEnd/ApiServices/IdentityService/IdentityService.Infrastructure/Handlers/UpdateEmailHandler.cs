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

            var userDbEntity = await _userRepository.GetUser(x => x.Email.Address == EmailAddress.Parse(updateEmailInfo.Email));
            if (userDbEntity == null)
                return new NotFoundObjectResult("Not found user with this email");

            _userService.ChangeEmail(userDbEntity, EmailAddress.Parse(updateEmailInfo.NewEmail));
            await _userRepository.Save(userDbEntity);

            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}