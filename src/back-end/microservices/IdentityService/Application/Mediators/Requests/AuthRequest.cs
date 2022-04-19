namespace IdentityService.Application.Mediators.Requests;

public class AuthRequest<T> : IRequest<IActionResult>
{
    private AuthRequest(T? value)
    {
        Body = value;
    }

    public T? Body { get; }

    public static AuthRequest<T> Create(T? body)
    {
        return new(body);
    }
}