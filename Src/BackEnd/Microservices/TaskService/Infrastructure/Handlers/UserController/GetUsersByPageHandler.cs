namespace TaskService.Infrastructure.Handlers.UserController;

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
            var count = await _userRepository.Count();

            return Ok(new {Total = count, Users = users.ToDto()});
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}
