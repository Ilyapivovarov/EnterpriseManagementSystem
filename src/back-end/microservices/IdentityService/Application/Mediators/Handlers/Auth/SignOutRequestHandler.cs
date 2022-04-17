namespace IdentityService.Application.Mediators.Handlers.Auth;

public class SignOutRequestHandler : IRequestHandler<AuthRequest<Guid>, IActionResult>
{
    private readonly ISessionRepository _sessionRepository;

    public SignOutRequestHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
    
    public async Task<IActionResult> Handle(AuthRequest<Guid> authRequest, CancellationToken cancellationToken)
    {
        var userGuid = authRequest.Body;
        var session = await _sessionRepository.GetSessionByUserGuid(userGuid);
        if (session == null)
            return new BadRequestResult();

        if (!await _sessionRepository.RemoveSession(session))
            return new BadRequestResult();

        return new OkResult();
    }
}