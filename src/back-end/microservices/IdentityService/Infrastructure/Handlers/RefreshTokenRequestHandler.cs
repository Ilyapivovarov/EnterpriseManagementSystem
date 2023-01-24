namespace IdentityService.Infrastructure.Handlers;

public sealed class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, IActionResult>
{
    private readonly ILogger<RefreshTokenRequestHandler> _logger;
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionService _sessionService;
    private readonly IUserRepository _userRepository;

    public RefreshTokenRequestHandler(ILogger<RefreshTokenRequestHandler> logger, ISessionRepository sessionRepository, 
        ISessionService sessionService, IUserRepository userRepository)
    {
        _logger = logger;
        _sessionRepository = sessionRepository;
        _sessionService = sessionService;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var oldSession = await _sessionRepository.GetAsync(request.RefreshToken);
            if (oldSession == null)
                return new NotFoundObjectResult("Not found session");
            
            var newSession = _sessionService.Refresh(oldSession.EmailAddress, oldSession.UserGuid, oldSession.Role);

            await _sessionRepository.SaveAsync(newSession);
            
            return new OkObjectResult(newSession.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}
