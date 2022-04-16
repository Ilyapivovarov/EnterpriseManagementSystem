namespace IdentityService.Infrastructure.Implementations.Repositories.Base;

public abstract class RepositoryBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<RepositoryBase> _logger;

    protected RepositoryBase(ApplicationDbContext dbContext, ILogger<RepositoryBase> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    protected T? LoadData<T>(Func<ApplicationDbContext, T?> loadFunc, string message)
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

    protected bool SaveData(Action<ApplicationDbContext> writeFunc, string message)
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
    
    protected async Task<bool> SaveDataAsync(Action<ApplicationDbContext> writeFunc, string message)
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
}