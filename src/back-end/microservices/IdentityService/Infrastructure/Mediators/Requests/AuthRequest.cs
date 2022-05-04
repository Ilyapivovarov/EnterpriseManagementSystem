namespace IdentityService.Infrastructure.Mediators.Requests;

public sealed class AuthRequest<T> : IRequest<IActionResult>
{
    private AuthRequest(T? value)
    {
        Body = value;
    }

    public T? Body { get; }

    public static AuthRequest<T> Create(T? body)
    {
        return new AuthRequest<T>(body);
    }
}