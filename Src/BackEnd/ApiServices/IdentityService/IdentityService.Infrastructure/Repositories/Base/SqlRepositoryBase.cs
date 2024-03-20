namespace IdentityService.Infrastructure.Repositories.Base;

public abstract class SqlRepositoryBase
{
    private readonly IIdentityDbContext _dbContext;
    private readonly ILogger<SqlRepositoryBase> _logger;

    protected SqlRepositoryBase(IIdentityDbContext dbContext, ILogger<SqlRepositoryBase> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    protected T? LoadData<T>(Func<IIdentityDbContext, T?> loadFunc, string message)
    {
        try
        {
            return loadFunc(_dbContext);
        }
        catch (Exception e)
        {
            _logger.LogError(e, message);
            return default;
        }
    }

    protected bool SaveData(Action<IIdentityDbContext> writeFunc, string message)
    {
        try
        {
            writeFunc(_dbContext);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, message);
            return false;
        }
    }

    protected async Task<bool> SaveDataAsync(Action<IIdentityDbContext> writeFunc, string message)
    {
        try
        {
            writeFunc(_dbContext);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, message);
            return false;
        }
    }

    protected async Task<T?> LoadDataAsync<T>(Func<IIdentityDbContext, T?> loadFunc,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await Task.Run(() => loadFunc(_dbContext), cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return default;
        }
    }

    protected async Task<bool> WriteDataAsync(Action<IIdentityDbContext> writeAction)
    {
        try
        {
            writeAction(_dbContext);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
}