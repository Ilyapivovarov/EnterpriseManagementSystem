namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

public interface IBus
{
    public Task SendMessageAsync<TMessage>(TMessage message)
        where TMessage : IMessage;
    
    public void SendMessage<TMessage>(TMessage message)
        where TMessage : IMessage;

    public Task PublishAsync<TIntegrationsEvent>(TIntegrationsEvent message)
        where TIntegrationsEvent : IIntegrationEvent;
}