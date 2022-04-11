using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Handlers;

public class SignInUserRequestHandler : IRequestHandler<SignInUserRequest, IActionResult>
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionBlService _sessionBlService;

    public SignInUserRequestHandler(IUserRepository userRepository, 
        ISessionRepository sessionRepository, ISessionBlService sessionBlService)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _sessionBlService = sessionBlService;
    }
    
    public async Task<IActionResult> Handle(SignInUserRequest request, CancellationToken cancellationToken)
    {
        var signInDto = request.SignInDto;
        if (signInDto == null)
            return new BadRequestObjectResult("Request body is empty");

        var user = await _userRepository.GetUserByEmailAndPasswordAsync(signInDto.Email, signInDto.Password);
        if (user == null)
            return new BadRequestObjectResult($"Not found user with email {signInDto.Email}");

        var session = await _sessionRepository.GetSessionByUserIdAsync(user.Id);
        var updatedSession = _sessionBlService.CreateOrUpdateSession(user, session);
        
        await _sessionRepository.SaveOrUpdateSessionAsync(updatedSession);

        return new OkObjectResult(updatedSession.ToDto());
    }
}