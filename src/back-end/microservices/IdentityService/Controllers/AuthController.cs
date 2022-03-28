using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Test()
    {
        var s = User.Identity?.Name;
        var a = User.Identity?.IsAuthenticated;

        return Ok(new {a, s});
    }
    
    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto? signIn)
    {
        var result = await _authService.SignInUserAsync(signIn);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return BadRequest(result.Error);
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<Session>> SignUp([FromBody] SignUpDto? signUp)
    {
        var result = await _authService.SignUpUserAsync(signUp);
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }
}