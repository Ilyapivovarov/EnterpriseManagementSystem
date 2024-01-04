namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

public interface IMessageHandler<in TMessage> : IConsumer<TMessage>
    where TMessage : class, IMessage
{
    public Task Handle(TMessage @event);
}