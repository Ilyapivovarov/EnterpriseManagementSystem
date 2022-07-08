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

            var userDbEntity = await _userRepository.GetUserByEmailAsync(updatePasswordInfo.Email);
            if (userDbEntity == null)
                return new NotFoundObjectResult($"Not found user with email {updatePasswordInfo.Email}");

            if (!updatePasswordInfo.NewPassword.Equals(updatePasswordInfo.ConfirmNewPassword, StringComparison.Ordinal))
                return new BadRequestObjectResult("Passwords in not same");

            var serviceResult = _userService.ChangePassword(userDbEntity, updatePasswordInfo.NewPassword);
            if (serviceResult.HasError)
                return new BadRequestObjectResult(serviceResult.Error);

            var saveResult = await _userRepository.UpadteUserAsync(userDbEntity);
            if (!saveResult)
                return new BadRequestObjectResult("Error while save new password");

            var @event = new SendSystemNotificationEvent(updatePasswordInfo.Email, "Password has been changed");
            // await _bus.Publish(@event, cancellationToken);

            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}