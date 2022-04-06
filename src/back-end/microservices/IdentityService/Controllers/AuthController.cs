using System.Security.Claims;
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

    public AuthController(IUserRepository userRepository, ISignInMediator signInMediator, 
        ISignUpMediator signUpMediator)
    {
        _userRepository = userRepository;
        _signInMediator = signInMediator;
        _signUpMediator = signUpMediator;
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
    public async Task<IActionResult> SignIn([FromBody] SignInDto? signIn)
    {
        return await _signInMediator.SignInUser(signIn);
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto? signUp)
    {
        return await _signUpMediator.SignUpUser(signUp);
    }
}