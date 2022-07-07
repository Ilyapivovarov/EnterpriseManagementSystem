namespace UserService.Infrastructure.Handlers.Base;

public abstract class HandlerBase<TRequest> : IRequestHandler<TRequest, IActionResult>
    where TRequest : IRequest<IActionResult>
{
    protected IActionResult Ok(object obj)
    {
        return new OkObjectResult(obj);
    }

    protected IActionResult Ok()
    {
        return new OkResult();
    }

    protected IActionResult Error(object obj)
    {
        return new BadRequestObjectResult(obj);
    }

    protected IActionResult Error()
    {
        return new BadRequestResult();
    }

    protected IActionResult NotFoud(object obj)
    {
        return new NotFoundObjectResult(obj);
    }

    protected IActionResult NotFoud()
    {
        return new NotFoundResult();
    }

    public abstract Task<IActionResult> Handle(TRequest request, CancellationToken cancellationToken);
}