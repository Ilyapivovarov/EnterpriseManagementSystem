namespace TaskService.Infrastructure.Consumers;

public sealed class SaveNewUserConsumer : IConsumer<SignUpUserIntegrationEvent>
{
    private readonly ILogger<SaveNewUserConsumer> _logger;
    private readonly IUserRepository _userRepository;

    public SaveNewUserConsumer(ILogger<SaveNewUserConsumer> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<SignUpUserIntegrationEvent> context)
    {
        try
        {
            using (_logger.BeginScope("Start consume {SignUpNewUserIntegrationEvent}",
                       nameof(SignUpUserIntegrationEvent)))
            {
                var account = context.Message.Account;
                var user = new UserDbEntity
                {
                    EmailAddress = account.Email,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    IdentityGuid = account.Guid,
                };

                await _userRepository.SaveUserDbEntityAsync(user);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}