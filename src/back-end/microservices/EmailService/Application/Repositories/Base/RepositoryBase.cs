namespace EmailService.Application.Repositories.Base;

public class RepositoryBase
{
    private readonly IEmailServiceDbContext _dbContext;
    private readonly ILogger _logger;

    public RepositoryBase(IEmailServiceDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    protected async Task<T?> LoadDataAsync<T>(Func<IEmailServiceDbContext, T> loadFunc, string errorMessage)
        where T : DbEntityBase
    {
        try
        {
            return await Task.Run(() => loadFunc(_dbContext));
        }
        catch (Exception e)
        {
            _logger.LogError(errorMessage, e);
            return default;
        }
    }

    protected async Task<bool> UpdateDataAsync(Action<IEmailServiceDbContext> action, string errorMessage)
    {
        try
        {
            action(_dbContext);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(errorMessage, e);
            return false;
        }
    }
}