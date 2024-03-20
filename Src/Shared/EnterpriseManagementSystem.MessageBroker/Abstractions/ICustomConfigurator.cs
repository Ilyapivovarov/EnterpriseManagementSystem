namespace EnterpriseManagementSystem.MessageBroker.Abstractions;

public interface ICustomConfigurator<TMessageHandler> : IConsumerConfigurator<TMessageHandler>
    where TMessageHandler : class
{
    
}