namespace TaskService.Infrastructure.Handlers.Base;

public abstract class RequestHandlerBase<T> : IRequestHandler<T, IActionResult>
    where T : IRequest<IActionResult>
{ 
    protected IActionResult Ok() => new OkResult();

    protected  IActionResult Ok(object obj) => new OkObjectResult(obj);
    
    protected  IActionResult Error() => new BadRequestResult();

    protected  IActionResult Error(object obj) => new BadRequestObjectResult(obj);

    public abstract Task<IActionResult> Handle(T request, CancellationToken cancellationToken);
}