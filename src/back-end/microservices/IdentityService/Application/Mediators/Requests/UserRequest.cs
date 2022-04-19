namespace IdentityService.Application.Mediators.Requests;

public sealed class UserControllerRequest<T> : IRequest<IActionResult>
{
    private UserControllerRequest(T? value)
    {
        Body = value;
    }

    public T? Body { get; }

    public static UserControllerRequest<T> Create(T? body)
    {
        return new(body);
    }
}