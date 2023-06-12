using MassTransit;

namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

public interface IEventHandler<in TIntegrationsEvent> : IConsumer<TIntegrationsEvent>
    where TIntegrationsEvent : class, ICustomConsumer
{
    public Task Handle(TIntegrationsEvent @event);
}

public abstract class EventHandlerBase<TIntegrationsEvent> : IEventHandler<TIntegrationsEvent>
    where TIntegrationsEvent : class, ICustomConsumer
{
    public async Task Consume(ConsumeContext<TIntegrationsEvent> context)
    {
        await Handle(context.Message);
    }

    public abstract Task Handle(TIntegrationsEvent @event);
}