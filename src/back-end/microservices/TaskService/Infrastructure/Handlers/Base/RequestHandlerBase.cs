using Microsoft.AspNetCore.Mvc;

namespace TaskService.Infrastructure.Handlers.Base;

public abstract class RequestHandlerBase
{
    protected IActionResult Ok() => new OkResult();

    protected  IActionResult Ok(object obj) => new OkObjectResult(obj);
    
    protected  IActionResult Error() => new BadRequestResult();

    protected  IActionResult Error(object obj) => new BadRequestObjectResult(obj);
}