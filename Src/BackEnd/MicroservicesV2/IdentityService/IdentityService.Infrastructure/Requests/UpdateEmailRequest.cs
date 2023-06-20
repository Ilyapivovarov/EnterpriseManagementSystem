using EnterpriseManagementSystem.Contracts;

namespace IdentityService.Infrastructure.Requests;

public sealed class UpdateEmailRequest : IRequest<IActionResult>
{
    public UpdateEmailRequest(UpdateEmailInfo updateEmailInfo)
    {
        UpdateEmailInfo = updateEmailInfo;
    }

    public UpdateEmailInfo UpdateEmailInfo { get; }
}