using EnterpriseManagementSystem.Contracts.Messages;
using IdentityService.Infrastructure.Mapper;

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

                var userWithSameEmail = await _userRepository.GetUserOrDefault(x => x.Email.Address == email);
                if (userWithSameEmail != null)
                    return new BadRequestObjectResult("Email already exist");

                var userRole = await _userRoleRepository.GetReaderRole();
                if (userRole == null)
                    return new NotFoundObjectResult("Reader role not found");

                var newUser = _userService.Create(email, password, userRole);
                await _userRepository.Save(newUser);
                
                var session = _sessionBlService.CreateSession(newUser.Email.Address, newUser.Guid, newUser.Role.Name);
                await _cacheService.SetAsync(session.RefreshToken, session.AccessToken, 
                    session.RefreshToken.GetExpirationTime());

                var @event = new SignUpUserMessage
                {
                    IdentityGuid = newUser.Guid,
                    DataBrith = DateTime.Now,
                    EmailAddress = newUser.Email.Address,
                    FirstName = firstName,
                    LastName = lastName,
                };
                await _bus.SendMessageAsync(@event);
            
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
