using EnterpriseManagementSystem.Cache.Abstractions;

namespace IdentityService.Infrastructure.Handlers;

public sealed class SignOutRequestHandler : IRequestHandler<SignOutRequest, IActionResult>
{
    private readonly ILogger<SignOutRequestHandler> _logger;
    private readonly ICacheService _cacheService;

    public SignOutRequestHandler(ILogger<SignOutRequestHandler> logger, ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task<IActionResult> Handle(SignOutRequest signOutRequest, CancellationToken cancellationToken)
    {
        try
        {
            // var userGuid = signOutRequest.Guid;
            // var session = await _sessionRepository.GetAsync(userGuid);
            // if (session == null)
            //     return new BadRequestResult();

            // if (!await _sessionRepository.RemoveSession(session))
            //     return new BadRequestResult();

            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}