namespace IdentityService.Infrastructure.Handlers;

public sealed class SignUpUserRequestHandler : IRequestHandler<SignUpRequest, IActionResult>
{
    private readonly IBus _bus;
    private readonly ILogger<SignUpUserRequestHandler> _logger;
    private readonly ISecurityService _securityService;
    private readonly ISessionService _sessionBlService;
    private readonly ISessionRepository _sessionRepository;
    private readonly IUserService _userBlService;
    private readonly IUserRepository _userRepository;

    public SignUpUserRequestHandler(ILogger<SignUpUserRequestHandler> logger, IUserService userBlService,
        IUserRepository userRepository, ISessionService sessionBlService, ISessionRepository sessionRepository,
        ISecurityService securityService, IBus bus)
    {
        _logger = logger;
        _userBlService = userBlService;
        _userRepository = userRepository;
        _sessionBlService = sessionBlService;
        _sessionRepository = sessionRepository;
        _securityService = securityService;
        _bus = bus;
    }

    public async Task<IActionResult> Handle(SignUpRequest authRequest, CancellationToken cancellationToken)
    {
        try
        {
            var signUpDto = authRequest.SignUp;

            var (firFristName, lastName, email, password, confirmPassword) = signUpDto;
            if (!password.Equals(confirmPassword, StringComparison.Ordinal))
                return new BadRequestObjectResult("Passwords is not same");

            var userWithSameEmail = await _userRepository.GetUserByEmailAsync(email);
            if (userWithSameEmail != null)
                return new BadRequestObjectResult("This email already exist");

            var encryptPassword = _securityService.EncryptPassword(password);
            var user = _userBlService.Create(email, encryptPassword);
            if (!await _userRepository.SaveUserAsync(user))
                return new BadRequestObjectResult("Error while save user");

            var session = _sessionBlService.CreateSession(user);
            await _sessionRepository.SaveOrUpdateSessionAsync(session);

            var @event = new SignUpUserIntegrationEvent(new Account(user.Guid, user.Email.Address, signUpDto.FirstName,
                signUpDto.LastName));
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