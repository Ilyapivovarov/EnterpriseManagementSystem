namespace IdentityService.Application.Mediators.Requests;

public class UserControllerRequest<T> : IRequest<IActionResult>
{
    public static UserControllerRequest<T> Create(T? body) => new(body); 
    
    private UserControllerRequest(T? value)
    {
        Body = value;
    }
    
    public T? Body { get; }
}