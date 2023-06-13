namespace EnterpriseManagementSystem.MessageBroker.Implementations;

public class Bus : IBus
{
    private readonly MassTransit.IBus _bus;

    public Bus(MassTransit.IBus bus)
    {
        _bus = bus;
    }

    public async Task SendMessageAsync<TMessage>(TMessage message)
        where TMessage : IMessage
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{typeof(TMessage).Name}"));
        await endpoint.Send(message);
    }
    
    public async Task PublishAsync<TIntegrationsEvent>(TIntegrationsEvent message)
        where TIntegrationsEvent : IIntegrationEvent
    {
        await _bus.Publish(message);
    }
}