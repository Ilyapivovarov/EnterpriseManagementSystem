namespace IdentityService.Infrastructure.Handlers.Auth;

public sealed class SignOutRequestHandler : IRequestHandler<SignOutRequest, IActionResult>
{
    private readonly ILogger<SignOutRequestHandler> _logger;
    private readonly ISessionRepository _sessionRepository;

    public SignOutRequestHandler(ILogger<SignOutRequestHandler> logger, ISessionRepository sessionRepository)
    {
        _logger = logger;
        _sessionRepository = sessionRepository;
    }

    public async Task<IActionResult> Handle(SignOutRequest signOutRequest, CancellationToken cancellationToken)
    {
        try
        {
            var userGuid = signOutRequest.Guid;
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