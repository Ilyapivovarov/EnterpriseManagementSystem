using Microsoft.AspNetCore.Authorization;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityHttpClient _identityHttpClient;

    public AuthController(IIdentityHttpClient identityHttpClient)
    {
        _identityHttpClient = identityHttpClient;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("sign-in")]
    public async Task<IActionResult> SingInUser([FromBody] SignIn signInDto)
    {
        return await _identityHttpClient.SignInAsync(signInDto);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("sign-up")]
    public async Task<IActionResult> SingInUser([FromBody] SignUp signUp)
    {
        return await _identityHttpClient.SignUpAsync(signUp);
    }

    [HttpDelete]
    [Route("sign-out")]
    public async Task<IActionResult> SignOutUser()
    {
        return await _identityHttpClient.SignOutUser();
    }
}