namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

public interface IBusInitializer
{
    public void SubscribeOnMessage<TMessage, TMessageHandler>(Action<IRegistrationContext, IConsumerConfigurator<TMessageHandler>>? configurator = null)
        where TMessage : class, IMessage
        where TMessageHandler : class, IMessageHandler<TMessage>;
    
    public void SubscribeOnEvent<TIntegrationEvent, TIntegrationEventHandler>(
        Action<IRegistrationContext, IConsumerConfigurator<TIntegrationEventHandler>>? configurator = null)
        where TIntegrationEvent : class, IIntegrationEvent
        where TIntegrationEventHandler : class, IIntegrationEventHandler<TIntegrationEvent>;
}
