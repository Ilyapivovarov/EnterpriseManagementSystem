namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

public interface IMessageHandler<in TMessage> : IConsumer<IMessage>
    where TMessage : class, IMessage
{
    public Task Handle(IMessage @event);
}