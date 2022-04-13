using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Handlers.UserController;

public class GetUserByGuidRequestHandler : IRequestHandler<Request<Guid>, IActionResult>
{
    private readonly IUserRepository _userRepository;

    public GetUserByGuidRequestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<IActionResult> Handle(Request<Guid> request, CancellationToken cancellationToken)
    {
        var guid = request.Body;
        if (guid == Guid.Empty)
            return new BadRequestObjectResult("Guid is empty");

        var user = await _userRepository.GetUserByGuidAsync(guid);
        if (user == null)
            return new BadRequestObjectResult($"Not found user with guid {guid}");

        return new OkObjectResult(user.ToDto());
    }
}