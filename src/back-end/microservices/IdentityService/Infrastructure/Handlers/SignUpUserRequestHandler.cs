namespace IdentityService.Infrastructure.Handlers;

public sealed class SignUpUserRequestHandler : IRequestHandler<SignUpRequest, IActionResult>
{
    private readonly ILogger<SignUpUserRequestHandler> _logger;
    private readonly IBus _bus;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly ISessionService _sessionBlService;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly ISessionRepository _sessionRepository;


    public SignUpUserRequestHandler(ILogger<SignUpUserRequestHandler> logger, IBus bus, IUserService userService,
        IUserRepository userRepository, ISessionService sessionBlService, IUserRoleRepository userRoleRepository,
        ISessionRepository sessionRepository)
    {
        _logger = logger;
        _bus = bus;
        _userService = userService;
        _userRepository = userRepository;
        _sessionBlService = sessionBlService;
        _userRoleRepository = userRoleRepository;
        _sessionRepository = sessionRepository;

    }

    public async Task<IActionResult> Handle(SignUpRequest authRequest, CancellationToken cancellationToken)
    {
        try
        {
            var signUpDto = authRequest.SignUp;

            var (fristName, lastName, email, password, confirmPassword) = signUpDto;
            if (password != confirmPassword)
                return new BadRequestObjectResult("Passwords is not same");

            var userServiceResult = await _userService.TryCreateUser(email, password);
            if (userServiceResult.Value == null)
                return new BadRequestObjectResult(userServiceResult.Error);

            var session = _sessionBlService.CreateSession(userServiceResult.Value);
            await _sessionRepository.SaveOrUpdateSessionAsync(session);

            var @event = new SignUpUserIntegrationEvent(new UserDataResponse(userServiceResult.Value.Guid, fristName,
                lastName, signUpDto.Email, null));
            await _bus.Publish(@event, cancellationToken);

            return new OkObjectResult(session.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}