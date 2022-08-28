using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;

namespace IdentityService.Infrastructure.Requests;

public sealed class SignUpRequest : IRequest<IActionResult>
{
    public SignUpRequest(SignUpDtoDto signUp)
    {
        SignUp = signUp;
    }

    public SignUpDtoDto SignUp { get; }
}