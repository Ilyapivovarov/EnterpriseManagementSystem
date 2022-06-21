namespace TaskService.Application.DbContexts;

public interface ITaskDbContext
{
    public DbSet<UserDbEntity> Users { get; }

    public DbSet<TaskDbEntity> Tasks { get; }

    public DbSet<TaskStatusDbEntity> TaskStatuses { get; }

    public DbSet<AttachmentDbEntity> Attachments { get; }

    public Task<int> SaveChagesAsync(CancellationToken cancellationToken = default);

    public int SaveChanges();
}