using Microsoft.AspNetCore.Mvc;

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
    
    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto? signIn)
    {
        var result = await _authService.SignInUserAsync(signIn);
        if (result.Value != null)
            return Ok(result.Value.ToDto());
        
        return BadRequest(result.Error);
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<Session>> SignUp([FromBody] SignUpDto? signUp)
    {
        var result = await _authService.SignUpUserAsync(signUp);
        if (result.Value != null)
            return Ok(result.Value.ToDto());

        return BadRequest(result.Error);
    }
}