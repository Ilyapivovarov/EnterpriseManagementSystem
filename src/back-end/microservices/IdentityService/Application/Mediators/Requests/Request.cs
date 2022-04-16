namespace IdentityService.Application.Mediators.Requests;

public class Request<T, TControllerType> : IRequest<IActionResult>
    where TControllerType : ControllerBase  
{
    public static Request<T, TControllerType> Create(T? body) => new(body); 
    
    private Request(T? value)
    {
        Body = value;
    }
    
    public T? Body { get; }

}