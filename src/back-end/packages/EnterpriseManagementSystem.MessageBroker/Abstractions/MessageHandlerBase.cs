namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

/// <summary>
/// Base class of message handlers.
/// </summary>
/// <typeparam name="TMessage"></typeparam>
public abstract class MessageHandlerBase<TMessage> : IMessageHandler<TMessage>
    where TMessage : class, IMessage
{
    /// <summary>
    /// Message handling method.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public abstract Task Handle(TMessage message);

    public async Task Consume(ConsumeContext<TMessage> context)
    {
        await Handle(context.Message);
    }
    
}