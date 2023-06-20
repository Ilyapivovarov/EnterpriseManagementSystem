using EnterpriseManagementSystem.MessageBroker;

namespace EnterpriseManagementSystem.Contracts.IntegrationEvents.Notifications;

public sealed record SendSystemNotificationEvent(string Recipient, string Message) : IIntegrationEvent;