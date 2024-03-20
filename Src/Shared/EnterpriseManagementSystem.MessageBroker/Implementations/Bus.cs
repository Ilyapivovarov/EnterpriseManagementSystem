using Microsoft.Extensions.Logging;

namespace EnterpriseManagementSystem.MessageBroker.Implementations;

public class Bus : IBus
{
    private readonly ILogger<IBus> _logger;
    private readonly MassTransit.IBus _bus;

    public Bus(ILogger<IBus> logger, MassTransit.IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task SendMessageAsync<TMessage>(TMessage message)
        where TMessage : IMessage
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{typeof(TMessage).Name}"));
        await endpoint.Send(message);
        
        _logger.LogInformation($"Message sent async: {typeof(TMessage).Name}");
    }

    public async void SendMessage<TMessage>(TMessage message) where TMessage : IMessage
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{typeof(TMessage).Name}"));
        await endpoint.Send(message);
        
        _logger.LogInformation($"Message sent: {typeof(TMessage).Name}");
    }

    public async Task PublishAsync<TIntegrationsEvent>(TIntegrationsEvent integrationEvent)
        where TIntegrationsEvent : IIntegrationEvent
    {
        await _bus.Publish(integrationEvent);
        
        _logger.LogInformation($"Message published async: {typeof(TIntegrationsEvent).Name}");
    }
}