namespace EnterpriseManagementSystem.MessageBroker.Implementations;

internal sealed class BusInitializer : IBusInitializer
{
    private readonly IBusRegistrationConfigurator _configurator;

    public BusInitializer(IBusRegistrationConfigurator configurator)
    {
        _configurator = configurator;
    }

    public void SubscribeOnMessage<TMessage, TMessageHandler>() 
        where TMessage : class, IMessage 
        where TMessageHandler : class, IMessageHandler<TMessage>
    {
        _configurator.AddConsumer<TMessageHandler>();
    }

    public void SubscribeOnEvent<TIntegrationEvent, TIntegrationEventHandler>() 
        where TIntegrationEvent : class, IIntegrationEvent 
        where TIntegrationEventHandler : class, IIntegrationEventHandler<TIntegrationEvent>
    {
        _configurator.AddConsumer<TIntegrationEventHandler>();
    }
}