using EnterpriseManagementSystem.Contracts.IntegrationEvents.Notifications;

namespace IdentityService.Infrastructure.Handlers;

public sealed class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, IActionResult>
{
    private readonly ILogger<UpdatePasswordHandler> _logger;
    private readonly IBus _bus;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public UpdatePasswordHandler(ILogger<UpdatePasswordHandler> logger, IBus bus, IUserRepository userRepository,
        IUserService userService)
    {
        _logger = logger;
        _bus = bus;
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<IActionResult> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var updatePasswordInfo = request.NewPasswordInfo;

            var userDbEntity = await _userRepository.GetUserOrDefault(x => x.Email.Address == EmailAddress.Parse(updatePasswordInfo.Email));
            if (userDbEntity == null)
                return new NotFoundObjectResult($"Not found user with email {updatePasswordInfo.Email}");

            if (!updatePasswordInfo.NewPassword.Equals(updatePasswordInfo.ConfirmNewPassword, StringComparison.Ordinal))
                return new BadRequestObjectResult("Passwords in not same");

            _userService.ChangePassword(userDbEntity, Password.Parse(updatePasswordInfo.NewPassword));

            await _userRepository.Save(userDbEntity);

            var @event = new SendSystemNotificationEvent(updatePasswordInfo.Email, "Password has been changed");
            await _bus.PublishAsync(@event);

            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}