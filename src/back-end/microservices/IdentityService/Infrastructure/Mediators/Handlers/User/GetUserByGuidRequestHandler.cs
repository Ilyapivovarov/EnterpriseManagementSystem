using IdentityService.Infrastructure.Mapper;
using IdentityService.Infrastructure.Mediators.Requests;

namespace IdentityService.Infrastructure.Mediators.Handlers.User;

public sealed class GetUserByGuidRequestHandler : IRequestHandler<UserControllerRequest<Guid>, IActionResult>
{
    private readonly ILogger<GetUserByGuidRequestHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUserByGuidRequestHandler(ILogger<GetUserByGuidRequestHandler> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }


    public async Task<IActionResult> Handle(UserControllerRequest<Guid> request, CancellationToken cancellationToken)
    {
        try
        {
            var guid = request.Body;
            if (guid == Guid.Empty)
                return new BadRequestObjectResult("Guid is empty");

            var user = await _userRepository.GetUserByGuidAsync(guid);
            if (user == null)
                return new BadRequestObjectResult($"Not found user with guid {guid}");

            return new OkObjectResult(user.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}