using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    [Route("sign-in")]
    public async Task<ActionResult> SingIn()
    {
        throw new NotImplementedException();
    }
}