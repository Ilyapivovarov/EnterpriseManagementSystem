using EnterpriseManagementSystem.MessageBroker;
using Microsoft.Extensions.Logging;

namespace EnterpriseManagementSystem.Contracts.Messages;

public record LogMessage : IMessage
{
    public required DateTime DateTime { get; init; }
    
    public required LogLevel Log { get; init; }

    public required string Method { get;  init; }

    public required string Message { get; init; }
}