using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthHttpClientService _authHttpClientService;

    public AuthController(IAuthHttpClientService authHttpClientService)
    {
        _authHttpClientService = authHttpClientService;
    }

    [HttpGet]
    public ActionResult Test()
    {
        return Ok();
    }


[HttpPost]
    [Route("sign-in")]
    public async Task<ActionResult> SingIn()
    {
        throw new NotImplementedException();
    }
}