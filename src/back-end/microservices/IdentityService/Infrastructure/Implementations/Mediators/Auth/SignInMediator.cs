using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Infrastructure.Implementations.Mediators.Auth;

public class SignInMediator : ISignInMediator
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionBlService _sessionBlService;
    private readonly ISessionRepository _sessionRepository;

    public SignInMediator(IUserRepository userRepository, ISessionBlService sessionBlService,
        ISessionRepository sessionRepository)
    {
        _userRepository = userRepository;
        _sessionBlService = sessionBlService;
        _sessionRepository = sessionRepository;
    }

    public async Task<ActionResult<SessionDto>> SignInUser(SignInDto? userDto)
    {
        if (userDto == null)
            return new BadRequestObjectResult("Request body is empty");

        var user = await _userRepository.GetUserByEmailAndPasswordAsync(userDto.Email, userDto.Password);
        if (user == null)
            return new BadRequestObjectResult($"Not found user with email {userDto.Email}");

        var session = await _sessionRepository.GetSessionByUserIdAsync(user.Id);
        var updatedSession = _sessionBlService.CreateOrUpdateSession(user, session);
        
        await _sessionRepository.SaveOrUpdateSessionAsync(updatedSession);

        return new OkObjectResult(updatedSession.ToDto());
    }
}