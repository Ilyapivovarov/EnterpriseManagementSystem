namespace IdentityService.Infrastructure.Handlers;

public sealed class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, IActionResult>
{
    private readonly ILogger<RefreshTokenRequestHandler> _logger;
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionService _sessionService;

    public RefreshTokenRequestHandler(ILogger<RefreshTokenRequestHandler> logger, ISessionRepository sessionRepository, ISessionService sessionService)
    {
        _logger = logger;
        _sessionRepository = sessionRepository;
        _sessionService = sessionService;
    }

    public async Task<IActionResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _sessionRepository.GetAsync(request.RefreshToken);
            if (session == null)
                return new NotFoundObjectResult("Not found session");

            var newSession = _sessionService.Refresh(session);
            
            return new OkObjectResult(newSession.ToDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}
