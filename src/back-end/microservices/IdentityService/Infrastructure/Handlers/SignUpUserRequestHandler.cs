using EnterpriseManagementSystem.Cache.Abstractions;

namespace IdentityService.Infrastructure.Handlers;

public sealed class SignUpUserRequestHandler : IRequestHandler<SignUpRequest, IActionResult>
{
    private readonly ILogger<SignUpUserRequestHandler> _logger;
    private readonly IBus _bus;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly ISessionService _sessionBlService;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly ICacheService _cacheService;


    public SignUpUserRequestHandler(ILogger<SignUpUserRequestHandler> logger, IBus bus, IUserService userService,
        IUserRepository userRepository, ISessionService sessionBlService, IUserRoleRepository userRoleRepository,
        ICacheService cacheService)
    {
        _logger = logger;
        _bus = bus;
        _userService = userService;
        _userRepository = userRepository;
        _sessionBlService = sessionBlService;
        _userRoleRepository = userRoleRepository;
        _cacheService = cacheService;
    }

    public async Task<IActionResult> Handle(SignUpRequest authRequest, CancellationToken cancellationToken)
    {
        try
        {
            using (_logger.BeginScope(this))
            {
                var signUpDto = authRequest.SignUp;

                var (firstName, lastName, email, password, confirmPassword) = signUpDto;
                if (password != confirmPassword)
                    return new NotFoundObjectResult("Passwords is not same");

                var userWithSameEmail = await _userRepository.GetUserByEmailAsync(email);
                if (userWithSameEmail != null)
                    return new BadRequestObjectResult("Email already exist");

                var userRole = await _userRoleRepository.GetReaderRole();
                if (userRole == null)
                    return new NotFoundObjectResult("Reader role not found");

                var newUser = _userService.Create(email, password, userRole);
                var saveResult = await _userRepository.SaveUserAsync(newUser);
                if (!saveResult)
                    return new NotFoundObjectResult("Error while save user");

                var session = _sessionBlService.CreateSession(newUser.Email.Address, newUser.Guid, newUser.Role.Name);
                await _cacheService.SetAsync(session.RefreshToken, session.AccessToken, 
                    session.RefreshToken.GetExpirationTime());

                var @event = new SignUpUserIntegrationEvent(new UserDataResponse(newUser.Guid, firstName,
                    lastName, signUpDto.Email, null));
                await _bus.PublishAsync(@event);
            
                return new OkObjectResult(session.ToDto());
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}
