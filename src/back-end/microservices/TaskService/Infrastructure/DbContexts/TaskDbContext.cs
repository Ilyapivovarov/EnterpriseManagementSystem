namespace TaskService.Infrastructure.DbContexts;

public sealed class TaskDbContext : DbContext, ITaskDbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();

    public DbSet<TaskDbEntity> Tasks => Set<TaskDbEntity>();

    public DbSet<TaskStatusDbEntity> TaskStatuses => Set<TaskStatusDbEntity>();

    public DbSet<AttachmentDbEntity> Attachments => Set<AttachmentDbEntity>();

    public async Task<int> SaveChagesAsync(CancellationToken cancellationToken = default) 
        => await SaveChangesAsync(cancellationToken);
}