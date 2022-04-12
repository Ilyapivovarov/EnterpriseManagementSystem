using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Handlers.AuthController;

public class SignOutRequestHandler : IRequestHandler<Request<Guid>, IActionResult>
{
    private readonly ISessionRepository _sessionRepository;

    public SignOutRequestHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
    
    public async Task<IActionResult> Handle(Request<Guid> request, CancellationToken cancellationToken)
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