using EnterpriseManagementSystem.Contracts.WebContracts.Response;
using EnterpriseManagementSystem.MessageBroker;

namespace EnterpriseManagementSystem.Contracts.IntegrationEvents;

public sealed record SignUpUserIntegrationEvent(UserDataResponse UserDataResponse) : IIntegrationsEvent
{
}