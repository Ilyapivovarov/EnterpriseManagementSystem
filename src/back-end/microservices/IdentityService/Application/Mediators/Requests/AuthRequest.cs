namespace IdentityService.Application.Mediators.Requests;

public class AuthRequest<T> : IRequest<IActionResult>
{
    public static AuthRequest<T> Create(T? body) => new(body); 
    
    private AuthRequest(T? value)
    {
        Body = value;
    }
    
    public T? Body { get; }

}