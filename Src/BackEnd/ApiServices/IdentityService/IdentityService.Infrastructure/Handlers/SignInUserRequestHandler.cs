using IdentityService.Infrastructure.Mapper;

namespace IdentityService.Infrastructure.Handlers;

public sealed class SignInUserRequestHandler : IRequestHandler<SignInRequest, IActionResult>
{
    private readonly ILogger<SignInUserRequestHandler> _logger;
    private readonly ISessionService _sessionService;
    private readonly ISecurityService _securityService;
    private readonly ICacheService _cacheService;
    private readonly IUserRepository _userRepository;

    public SignInUserRequestHandler(ILogger<SignInUserRequestHandler> logger, IUserRepository userRepository,
        ISessionService sessionService, ISecurityService securityService, ICacheService cacheService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _sessionService = sessionService;
        _securityService = securityService;
        _cacheService = cacheService;
    }

    public async Task<IActionResult> Handle(SignInRequest signInRequest, CancellationToken cancellationToken)
    {
        try
        {
            using (_logger.BeginScope(this))
            {
                var signInDto = signInRequest.SignInDto;
                var hashPassword = _securityService.EncryptPasswordOrException(signInDto.Password.Value);
                var user = await _userRepository.GetUser(x => x.Email.Address == signInDto.Email 
                                                              && x.Password == Password.Parse(hashPassword));
                
                _logger.LogInformation($"Found user with email {0}", user.Email.Address.Value);
                var session = _sessionService.CreateSession(user.Email.Address, user.Guid, user.Role.Name);

                await _cacheService.SetAsync(session.RefreshToken.ToString(), session.AccessToken.ToString(),
                    session.RefreshToken.GetExpirationTime());

                var sessionDto = session.ToDto();
                _logger.LogInformation($"A new session has been successfully created: {sessionDto}");
                return new OkObjectResult(sessionDto);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}