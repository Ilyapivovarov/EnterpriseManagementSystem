namespace IdentityService.Infrastructure.Handlers;

public sealed class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, IActionResult>
{
    private readonly ILogger<RefreshTokenRequestHandler> _logger;
    private readonly ISessionService _sessionService;

    public RefreshTokenRequestHandler(ILogger<RefreshTokenRequestHandler> logger, ISessionService sessionService)
    {
        _logger = logger;
        _sessionService = sessionService;
    }

    public async Task<IActionResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sessionService.RefreshToken(request.RefreshToken);
            if (result.HasError)
                return new BadRequestObjectResult(result.Error);

            return new OkObjectResult(result.Value?.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}
