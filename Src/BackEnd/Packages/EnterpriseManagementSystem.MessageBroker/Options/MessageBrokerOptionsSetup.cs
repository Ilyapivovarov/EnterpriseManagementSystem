namespace EnterpriseManagementSystem.MessageBroker.Options;

public class MessageBrokerOptionsSetup : IConfigureOptions<MessageBrokerOptions>
{
    private const string MessageBrokerSectionName = "MessageBroker";
    
    private readonly IConfiguration _configuration;

    public MessageBrokerOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(MessageBrokerOptions options)
    {
        _configuration.GetRequiredSection(MessageBrokerSectionName)
            .Bind(options);
    }
}