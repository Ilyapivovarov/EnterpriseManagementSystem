namespace TaskService.Infrastructure.Handlers.Base;

public abstract class RequestHandlerBase<T> : IRequestHandler<T, IActionResult>
    where T : IRequest<IActionResult>
{
    public abstract Task<IActionResult> Handle(T request, CancellationToken cancellationToken);

    protected IActionResult Ok()
    {
        return new OkResult();
    }

    protected IActionResult Ok(object obj)
    {
        return new OkObjectResult(obj);
    }

    protected IActionResult Error()
    {
        return new BadRequestResult();
    }

    protected IActionResult Error(object obj)
    {
        return new BadRequestObjectResult(obj);
    }
}