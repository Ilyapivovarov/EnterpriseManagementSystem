using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public AuthController(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
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
    public async Task<ActionResult<SessionDto>> SignIn([FromBody] SignInDto? signIn)
    {
        var result = await _authService.SignInUserAsync(signIn);
        if (result.Value != null)
            return Ok(result.Value.ToDto());
        
        return BadRequest(result.Error);
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<SessionDto>> SignUp([FromBody] SignUpDto? signUp)
    {
        var result = await _authService.SignUpUserAsync(signUp);
        if (result.Value != null)
            return Ok(result.Value.ToDto());

        return BadRequest(result.Error);
    }
}