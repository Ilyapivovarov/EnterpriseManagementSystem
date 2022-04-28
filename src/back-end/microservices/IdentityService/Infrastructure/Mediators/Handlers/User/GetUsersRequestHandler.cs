using IdentityService.Infrastructure.Mediators.Requests;

namespace IdentityService.Infrastructure.Mediators.Handlers.User;

public sealed class GetUsersRequestHandler : IRequestHandler<UserControllerRequest<int>, IActionResult>
{
    private readonly ILogger<GetUsersRequestHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUsersRequestHandler(ILogger<GetUsersRequestHandler> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Handle(UserControllerRequest<int> request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _userRepository.GetUsersByPageAsync(request.Body);
            return new OkObjectResult(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}