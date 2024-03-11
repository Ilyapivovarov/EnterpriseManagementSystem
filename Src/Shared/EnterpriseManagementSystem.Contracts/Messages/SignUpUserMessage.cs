using EnterpriseManagementSystem.MessageBroker.Abstractions;

namespace EnterpriseManagementSystem.Contracts.Messages;

public sealed record SignUpUserMessage : IMessage
{
    public Guid IdentityGuid { get; init; }
    
    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }

    [JsonConverter(typeof(EmailAddressJsonConverter))]
    public required EmailAddress EmailAddress { get; init; }

    public required DateTime? DataBrith { get; init; }
}