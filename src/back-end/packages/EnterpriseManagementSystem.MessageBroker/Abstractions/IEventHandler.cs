namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

public interface IIntegrationEventHandler<in TIntegrationsEvent> : IConsumer<TIntegrationsEvent>
    where TIntegrationsEvent : class, IIntegrationEvent
{
    public Task Handle(TIntegrationsEvent @event);
}