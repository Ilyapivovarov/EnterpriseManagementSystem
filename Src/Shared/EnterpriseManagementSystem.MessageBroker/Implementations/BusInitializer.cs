using System.Reflection;
using MassTransit.Configuration;
namespace EnterpriseManagementSystem.MessageBroker.Implementations;

internal sealed class BusInitializer : IBusInitializer
{
    private readonly IBusRegistrationConfigurator _configurator;

    public BusInitializer(IBusRegistrationConfigurator configurator)
    {
        _configurator = configurator;
    }

    public void SubscribeOnMessage<TMessage, TMessageHandler>(Action<ICustomRegistrationContext, ICustomConfigurator<TMessageHandler>>? configurator = null)
        where TMessage : class, IMessage
        where TMessageHandler : class, IMessageHandler<TMessage>
    {
        _configurator.AddConsumer(configurator as Action<IRegistrationContext, IConsumerConfigurator<TMessageHandler>>)
            .Endpoint(x => x.Name = typeof(TMessage).Name);
    }

    public void SubscribeOnEvent<TIntegrationEvent, TIntegrationEventHandler>(
        Action<IRegistrationContext, IConsumerConfigurator<TIntegrationEventHandler>>? configurator = null)
        where TIntegrationEvent : class, IIntegrationEvent
        where TIntegrationEventHandler : class, IIntegrationEventHandler<TIntegrationEvent>
    {
        _configurator.AddConsumer(configurator);
    }
}
