using MassTransit;

namespace EnterpriseManagementSystem.MessageBroker;

public interface IMessage : ICustomConsumer
{
    
}

public interface IIntegrationsEvent : ICustomConsumer
{
   
}

public interface ICustomConsumer : IConsumer
{
    
}