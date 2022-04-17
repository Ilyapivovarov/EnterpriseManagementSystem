namespace IdentityService.Application.Mediators.Handlers.User;

public class GetUsersRequestHandler : IRequestHandler<UserControllerRequest<int>, IActionResult>
{
    private readonly IUserRepository _userRepository;

    public GetUsersRequestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Handle(UserControllerRequest<int> request, CancellationToken cancellationToken)
    {
        var data = await _userRepository.GetUsersByPageAsync(request.Body);
        return new OkObjectResult(data);
    }
}