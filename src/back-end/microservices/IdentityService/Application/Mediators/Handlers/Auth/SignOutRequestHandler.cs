namespace IdentityService.Application.Mediators.Handlers.Auth;

public sealed class SignOutRequestHandler : IRequestHandler<AuthRequest<Guid>, IActionResult>
{
    private readonly ILogger<SignOutRequestHandler> _logger;
    private readonly ISessionRepository _sessionRepository;

    public SignOutRequestHandler(ILogger<SignOutRequestHandler> logger, ISessionRepository sessionRepository)
    {
        _logger = logger;
        _sessionRepository = sessionRepository;
    }

    public async Task<IActionResult> Handle(AuthRequest<Guid> authRequest, CancellationToken cancellationToken)
    {
        try
        {
            var userGuid = authRequest.Body;
            var session = await _sessionRepository.GetSessionByUserGuid(userGuid);
            if (session == null)
                return new BadRequestResult();

            if (!await _sessionRepository.RemoveSession(session))
                return new BadRequestResult();

            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}