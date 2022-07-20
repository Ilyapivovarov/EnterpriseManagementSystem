using EnterpriseManagementSystem.Contracts.WebContracts.Response;

namespace EnterpriseManagementSystem.Contracts.IntegrationEvents;

public sealed record SignUpUserIntegrationEvent(UserDataResponse UserDataResponse)
{
    public override string? ToString()
    {
        return base.ToString();
    }
}