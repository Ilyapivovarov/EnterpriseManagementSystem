namespace EnterpriseManagementSystem.Contracts.IntegrationEvents.Notifications;

public readonly record struct SendSystemNotificationEvent(string Recipient, string Message);