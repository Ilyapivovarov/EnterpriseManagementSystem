namespace IdentityService.Application.Mediators.Handlers.Auth;

public sealed class SignUpUserRequestHandler : IRequestHandler<AuthRequest<SignUp>, IActionResult>
{
    private readonly ILogger<SignUpUserRequestHandler> _logger;
    private readonly ISecurityService _securityService;
    private readonly ISessionBlService _sessionBlService;
    private readonly ISessionRepository _sessionRepository;
    private readonly IUserBlService _userBlService;
    private readonly IUserRepository _userRepository;

    public SignUpUserRequestHandler(ILogger<SignUpUserRequestHandler> logger, IUserBlService userBlService,
        IUserRepository userRepository,
        ISessionBlService sessionBlService, ISessionRepository sessionRepository, ISecurityService securityService)
    {
        _logger = logger;
        _userBlService = userBlService;
        _userRepository = userRepository;
        _sessionBlService = sessionBlService;
        _sessionRepository = sessionRepository;
        _securityService = securityService;
    }

    public async Task<IActionResult> Handle(AuthRequest<SignUp> authRequest, CancellationToken cancellationToken)
    {
        try
        {
            var signUpDto = authRequest.Body;
            if (signUpDto == null)
                return new BadRequestObjectResult("Request body is empty");

            var (email, password, confirmPassword) = signUpDto;
            if (!password.Equals(confirmPassword, StringComparison.Ordinal))
                return new BadRequestObjectResult("Passwords is not same");

            var userWithSameEmail = await _userRepository.GetUserByEmailAsync(email);
            if (userWithSameEmail != null)
                return new BadRequestObjectResult("This email already exist");

            var encryptPassword = _securityService.EncryptPassword(password);
            var user = _userBlService.CreateUser(email, encryptPassword);
            if (!await _userRepository.SaveUserAsync(user))
                return new BadRequestObjectResult("Error while save user");

            var session = _sessionBlService.CreateSession(user);

            await _sessionRepository.SaveOrUpdateSessionAsync(session);

            return new OkObjectResult(session.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}