using System.IdentityModel.Tokens.Jwt;

namespace IdentityService.Infrastructure.Handlers;

public sealed class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, IActionResult>
{
    private readonly ILogger<RefreshTokenRequestHandler> _logger;
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionService _sessionService;
    private readonly ICacheService _cacheService;

    public RefreshTokenRequestHandler(ILogger<RefreshTokenRequestHandler> logger, ISessionRepository sessionRepository, 
        ISessionService sessionService, ICacheService cacheService)
    {
        _logger = logger;
        _sessionRepository = sessionRepository;
        _sessionService = sessionService;
        _cacheService = cacheService;
    }

    public async Task<IActionResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var accessToken = await _sessionRepository.GetAsync(request.RefreshToken);
            if (accessToken == null)
                return new NotFoundObjectResult("Not found session");
            
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(accessToken);
            var newSession = _sessionService.Refresh(jsonToken.Claims.ToArray());

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
