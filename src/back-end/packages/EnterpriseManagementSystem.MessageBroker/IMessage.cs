using MassTransit;

namespace EnterpriseManagementSystem.MessageBroker;

public interface IMessage : IConsumer
{
}

public interface IIntegrationsEvent : IConsumer
{
   
}