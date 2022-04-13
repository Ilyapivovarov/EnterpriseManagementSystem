namespace IdentityService.Application.Mediators.Handlers.Auth;

public class SignOutRequestHandler : IRequestHandler<Request<Guid, AuthController>, IActionResult>
{
    private readonly ISessionRepository _sessionRepository;

    public SignOutRequestHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
    
    public async Task<IActionResult> Handle(Request<Guid, AuthController> request, CancellationToken cancellationToken)
    {
        var userGuid = request.Body;
        var session = await _sessionRepository.GetSessionByUserGuid(userGuid);
        if (session == null)
            return new BadRequestResult();

        if (!await _sessionRepository.RemoveSession(session))
            return new BadRequestResult();

        return new OkResult();
    }
}