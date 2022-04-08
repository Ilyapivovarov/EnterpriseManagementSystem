using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Infrastructure.Implementations.Mediators.Auth;

public class SignOutMediator : ISignOutMediator
{
    private readonly ISessionRepository _sessionRepository;

    public SignOutMediator(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<ActionResult> SignOutUser(Guid userGuid)
    {
        var session = await _sessionRepository.GetSessionByUserGuid(userGuid);
        if (session == null)
            return new BadRequestResult();

        if (!await _sessionRepository.RemoveSession(session))
            return new BadRequestResult();

        return new OkResult();
    }
}