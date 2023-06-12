using EnterpriseManagementSystem.MessageBroker.Abstractions;
using MassTransit;
using MassTransit.Internals;

namespace EnterpriseManagementSystem.MessageBroker.Implementations;

internal sealed class BusInitializer : IBusInitializer
{
    private readonly IBusRegistrationConfigurator _configurator;

    public BusInitializer(IBusRegistrationConfigurator configurator)
    {
        _configurator = configurator;
    }

    public void Subscribe<TMessage, TMessageHandler>()
        where TMessage : class, IMessage
    {
        _configurator.AddConsumer<TMessage>()
            .Endpoint(x => x.Name = $"{typeof(TMessage).GetTypeName()}");
    }
}