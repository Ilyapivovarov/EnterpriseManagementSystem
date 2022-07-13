using UserService.Core.DbEntities.Base;

namespace UserService.Infrastructure.Repository.Base;

public abstract class RepositoryBase
{
    private readonly ILogger<RepositoryBase> _logger;
    private readonly IUserDbContext _userDbContext;

    protected RepositoryBase(ILogger<RepositoryBase> logger, IUserDbContext userDbContext)
    {
        _logger = logger;
        _userDbContext = userDbContext;
    }

    protected async Task<T?> LoadDataAsync<T>(Func<IUserDbContext, T?> loadFunc,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await Task.Run(() => loadFunc(_userDbContext), cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return default;
        }
    }
    
    protected async Task<bool> WriteDataAsync(Action<IUserDbContext> writeAction)
    {
        try
        {
            writeAction(_userDbContext);
            await _userDbContext.SaveChangesAsync();
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
}