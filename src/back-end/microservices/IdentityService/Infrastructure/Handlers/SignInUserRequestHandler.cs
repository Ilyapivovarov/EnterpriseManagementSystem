namespace IdentityService.Infrastructure.Handlers;

public sealed class SignInUserRequestHandler : IRequestHandler<SignInRequest, IActionResult>
{
    private readonly ILogger<SignInUserRequestHandler> _logger;
    private readonly ISessionService _sessionBlService;
    private readonly ISecurityService _securityService;
    private readonly ISessionRepository _sessionRepository;
    private readonly IUserRepository _userRepository;

    public SignInUserRequestHandler(ILogger<SignInUserRequestHandler> logger, IUserRepository userRepository,
        ISessionRepository sessionRepository, ISessionService sessionBlService, ISecurityService securityService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _sessionBlService = sessionBlService;
        _securityService = securityService;
    }

    public async Task<IActionResult> Handle(SignInRequest signInRequest, CancellationToken cancellationToken)
    {
        try
        {
            var signInDto = signInRequest.SignInDto;
            var hashPassword = _securityService.EncryptPasswordOrException(signInDto.Password.Value);
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(signInDto.Email, Password.Parse(hashPassword));
            if (user == null)
                return new NotFoundObjectResult("Incorrect email or password");

            var session = _sessionBlService.CreateSession(user.Email.Address, user.Guid, user.Role.Name);

            await _sessionRepository.SaveAsync(session);

            return new OkObjectResult(session.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}