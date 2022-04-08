using System.Security.Claims;
using EnterpriseManagementSystem.Contracts.WebContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ISignInMediator _signInMediator;
    private readonly ISignUpMediator _signUpMediator;
    private readonly ISignOutMediator _signOutMediator;

    public AuthController(IUserRepository userRepository, ISignInMediator signInMediator, 
        ISignUpMediator signUpMediator, ISignOutMediator signOutMediator)
    {
        _userRepository = userRepository;
        _signInMediator = signInMediator;
        _signUpMediator = signUpMediator;
        _signOutMediator = signOutMediator;
    }

    [HttpGet]
    [Authorize]
    public ActionResult Test()
    {
        var email = User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
        var user =  _userRepository.GetUserByEmail(email);
        return Ok(user);
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<ActionResult<SessionDto>> SignInUser([FromBody] SignInDto? signIn)
    {
        return await _signInMediator.SignInUser(signIn);
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<SessionDto>> SignUpUser([FromBody] SignUpDto? signUp)
    {
        return await _signUpMediator.SignUpUser(signUp);
    }

    [Authorize]
    [HttpDelete]
    [Route("sign-out")]
    public async Task<ActionResult> SignOutUser()
    {
        var guidStr = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return await _signOutMediator.SignOutUser(Guid.Parse(guidStr));
    }
}