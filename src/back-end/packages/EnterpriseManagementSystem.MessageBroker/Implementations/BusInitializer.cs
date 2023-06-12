using EnterpriseManagementSystem.MessageBroker.Abstractions;
using MassTransit;

namespace EnterpriseManagementSystem.MessageBroker.Implementations;

internal sealed class BusInitializer : IBusInitializer
{
    private readonly IBusRegistrationConfigurator _configurator;

    public BusInitializer(IBusRegistrationConfigurator configurator)
    {
        _configurator = configurator;
    }

    public void Subscribe<TMessage, TMessageHandler>()
        where TMessage : class, ICustomConsumer
        where TMessageHandler : class, IEventHandler<TMessage>
    {
        _configurator.AddConsumer<TMessageHandler>();
    }
}