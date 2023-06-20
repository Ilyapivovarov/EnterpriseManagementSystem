namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

public interface IBusInitializer
{
    public void SubscribeOnMessage<TMessage, TMessageHandler>()
        where TMessage : class, IMessage
        where TMessageHandler : class, IMessageHandler<TMessage>;
    
    public void SubscribeOnEvent<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : class, IIntegrationEvent
        where TIntegrationEventHandler : class, IIntegrationEventHandler<TIntegrationEvent>;
}