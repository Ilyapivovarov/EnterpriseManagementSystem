using EnterpriseManagementSystem.MessageBroker;
using EnterpriseManagementSystem.MessageBroker.Abstractions;

namespace EnterpriseManagementSystem.Contracts.Messages;

public record LogMessage : IMessage
{
    public required string AppName { get; set; }
    
    public required DateTime DateTime { get; init; } = DateTime.Now;

    public required string Level { get; init; }

    public required string Method { get;  init; }

    public required string Message { get; init; }
}