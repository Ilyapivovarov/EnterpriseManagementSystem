namespace IdentityService.Application.Mediators.Handlers.User;

public class GetUsersRequestHandler : IRequestHandler<Request<int, UserController>, IActionResult>
{
    private readonly IUserRepository _userRepository;

    public GetUsersRequestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Handle(Request<int, UserController> request, CancellationToken cancellationToken)
    {
        var data = await _userRepository.GetUsersByPageAsync(request.Body);
        return new OkObjectResult(data);
    }
}