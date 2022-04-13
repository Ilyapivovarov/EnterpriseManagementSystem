using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Handlers.UserController;

public class GetUsersRequestHandler : IRequestHandler<Request<int>, IActionResult>
{
    private readonly IUserRepository _userRepository;

    public GetUsersRequestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IActionResult> Handle(Request<int> request, CancellationToken cancellationToken)
    {
        var data = await _userRepository.GetUsersByPageAsync(request.Body);
        return new OkObjectResult(data);
    }
}