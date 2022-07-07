using UserService.Application.Repository;
using UserService.Infrastructure.Handlers.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.Infrastructure.Handlers;

public sealed class GetUserByGuidHandler : HandlerBase<GetUserGuidRequest>
{
    private readonly ILogger<GetUserByGuidHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUserByGuidHandler(ILogger<GetUserByGuidHandler> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public override async Task<IActionResult> Handle(GetUserGuidRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var guid = request.Guid;

            var userDbEntity = await _userRepository.GetByGuidAsync(guid);

            return userDbEntity == null ? NotFoud("Not found user with guid") : Ok(userDbEntity.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}