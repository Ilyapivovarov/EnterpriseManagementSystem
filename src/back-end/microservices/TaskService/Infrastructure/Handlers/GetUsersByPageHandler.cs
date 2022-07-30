namespace TaskService.Infrastructure.Handlers;

public sealed class GetUsersByPageHandler : RequestHandlerBase<GetUsersByPageRequest>
{
    private readonly ILogger<GetUsersByPageHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUsersByPageHandler(ILogger<GetUsersByPageHandler> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;

    }

    public override async Task<IActionResult> Handle(GetUsersByPageRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository.GetUsersByPage(request.Count * request.PageNumber - request.Count,
                request.Count);
            return Ok(users.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}