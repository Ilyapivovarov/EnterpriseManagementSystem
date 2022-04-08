using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Infrastructure.Implementations.Mediators.Auth;

public class SignUpMediator : ISignUpMediator
{
    private readonly IUserBlService _userBlService;
    private readonly IUserRepository _userRepository;
    private readonly ISessionBlService _sessionBlService;
    private readonly ISessionRepository _sessionRepository;
    private readonly ISecurityService _securityService;

    public SignUpMediator(IUserBlService userBlService, IUserRepository userRepository,
        ISessionBlService sessionBlService, ISessionRepository sessionRepository, ISecurityService securityService)
    {
        _userBlService = userBlService;
        _userRepository = userRepository;
        _sessionBlService = sessionBlService;
        _sessionRepository = sessionRepository;
        _securityService = securityService;
    }

    public async Task<ActionResult<SessionDto>> SignUpUser(SignUpDto? signUpDto)
    {
        if (signUpDto == null)
            return new BadRequestObjectResult("Request body is empty");

        var (email, password, confirmPassword) = signUpDto;
        if (!password.Equals(confirmPassword, StringComparison.Ordinal))
            return new BadRequestObjectResult("Passwords is not same");

        var userWithSameEmail = await _userRepository.GetUserByEmailAsync(email);
        if (userWithSameEmail != null)
            return new BadRequestObjectResult("This email already exist");

        var encryptPassword = _securityService.EncryptPassword(password);
        var user = _userBlService.CreateUser(email, encryptPassword);
        if (!await _userRepository.SaveUserAsync(user))
            return new BadRequestObjectResult("Error while save user");
        
        var session = _sessionBlService.CreateSession(user);
        
        await _sessionRepository.SaveOrUpdateSessionAsync(session);
        
        return new OkObjectResult(session.ToDto());
    }
}