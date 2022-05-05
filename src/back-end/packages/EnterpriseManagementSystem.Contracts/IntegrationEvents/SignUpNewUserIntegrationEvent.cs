namespace EnterpriseManagementSystem.Contracts.IntegrationEvents;

public record SignUpNewUserIntegrationEvent(string From, string To, string Subject, string Body);