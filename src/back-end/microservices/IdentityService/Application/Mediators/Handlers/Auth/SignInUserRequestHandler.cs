namespace IdentityService.Application.Mediators.Handlers.Auth;

public class SignInUserRequestHandler : IRequestHandler<AuthRequest<SignIn>, IActionResult>
{
    private readonly ILogger<SignInUserRequestHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionBlService _sessionBlService;

    public SignInUserRequestHandler(ILogger<SignInUserRequestHandler> logger, IUserRepository userRepository, 
        ISessionRepository sessionRepository, ISessionBlService sessionBlService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _sessionBlService = sessionBlService;
    }
    
    public async Task<IActionResult> Handle(AuthRequest<SignIn> authRequest, CancellationToken cancellationToken)
    {
        try
        {
            var signInDto = authRequest.Body;
            if (signInDto == null)
                return new BadRequestObjectResult("Request body is empty");

            var user = await _userRepository.GetUserByEmailAndPasswordAsync(signInDto.Email, signInDto.Password);
            if (user == null)
                return new BadRequestObjectResult($"Not found user with email {signInDto.Email}");

            var session = await _sessionRepository.GetSessionByUserIdAsync(user.Id);
            var updatedSession = _sessionBlService.CreateOrUpdateSession(user, session);
        
            await _sessionRepository.SaveOrUpdateSessionAsync(updatedSession);

            return new OkObjectResult(updatedSession.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}