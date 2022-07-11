using UserService.Infrastructure.Mapper;

namespace UserService.Infrastructure.Handlers;

public sealed class GetUsersByPageHandler : HandlerBase<GetUsersByPageRequest>
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
            var pageNumber = request.PageNumber;

            if (pageNumber == 0)
                return Error("Page number can not be zero");

            var rangeEnd = pageNumber * 10 - 1;
            var rangeStart = rangeEnd - 9;

            var users = await _userRepository.GetUsersByRange(rangeStart, rangeEnd);

            return Ok(users.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}