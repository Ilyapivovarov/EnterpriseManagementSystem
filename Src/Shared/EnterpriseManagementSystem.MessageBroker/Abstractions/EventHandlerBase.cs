namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

/// <summary>
/// Base class of event handlers.
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
public abstract class IntegrationEventHandlerBase<TIntegrationEvent> : IIntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : class, IIntegrationEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        await Handle(context.Message);
    }

    /// <summary>
    /// Event handling method.
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    public abstract Task Handle(TIntegrationEvent @event);
}