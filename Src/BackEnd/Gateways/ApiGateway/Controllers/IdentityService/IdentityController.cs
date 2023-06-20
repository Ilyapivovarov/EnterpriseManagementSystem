using Microsoft.AspNetCore.Authorization;

namespace ApiGateway.Controllers.IdentityService;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityServiceHttpClient _identityServiceHttpClient;

    public AuthController(IIdentityServiceHttpClient identityServiceHttpClient)
    {
        _identityServiceHttpClient = identityServiceHttpClient;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("sign-in")]
    public async Task<IActionResult> SingInUser([FromBody] SignInDto signInDtoDto)
    {
        return await _identityServiceHttpClient.SignInAsync(signInDtoDto);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("sign-up")]
    public async Task<IActionResult> SingInUser([FromBody] SignUpDtoDto signUp)
    {
        return await _identityServiceHttpClient.SignUpAsync(signUp);
    }

    [HttpDelete]
    [Route("sign-out")]
    public async Task<IActionResult> SignOutUser()
    {
        return await _identityServiceHttpClient.SignOutUser();
    }

    [HttpPut]
    [AllowAnonymous]
    [Route("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        return await _identityServiceHttpClient.RefreshToken(refreshTokenDto);
    }
}
