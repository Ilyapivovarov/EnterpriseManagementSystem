using EmailService.Application.DbContext;

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

    protected T? LoadData<T>(Func<IEmailServiceDbContext, T> loadFunc, string errorMessage)
        where T : DbEntityBase
    {
        try
        {
            return loadFunc(_dbContext);
        }
        catch (Exception e)
        {
            _logger.LogError(errorMessage, e);
            return default;
        }
    }

    protected async Task<bool> UpdateData(Action<IEmailServiceDbContext> action, string errorMessage)
    {
        try
        {
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(errorMessage, e);
            return false;
        }
    }
}