namespace TaskService.Infrastructure.Repositories.Base;

public abstract class RepositoryBase
{
    private readonly ILogger<RepositoryBase> _logger;
    private readonly ITaskDbContext _taskDbContext;

    protected RepositoryBase(ITaskDbContext taskDbContext, ILogger<RepositoryBase> logger)
    {
        _taskDbContext = taskDbContext;
        _logger = logger;
    }

    protected async Task<T?> LoadDataAsync<T>(Func<ITaskDbContext, T> loadFunc,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await Task.Run(() => loadFunc(_taskDbContext), cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return default;
        }
    }

    protected async Task<bool> WriteDataAsync(Action<ITaskDbContext> writeAction,
        CancellationToken cancellationToken = default)
    {
        try
        {
            writeAction(_taskDbContext);
            await _taskDbContext.SaveChagesAsync(cancellationToken);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
}