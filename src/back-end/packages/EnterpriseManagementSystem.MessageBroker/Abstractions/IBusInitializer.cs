namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

public interface IBusInitializer
{
    public void Subscribe<TMessage, TMessageHandler>()
        where TMessage : class, ICustomConsumer
        where TMessageHandler : class, IEventHandler<TMessage>;
}