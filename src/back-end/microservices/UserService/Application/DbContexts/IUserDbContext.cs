namespace UserService.Application.DbContexts;

public interface IUserDbContext
{
    public DbSet<UserDbEntity> Users { get; }

    public DbSet<EmployeeDbEntity> Eployees { get; }

    public DbSet<PositionDbEntity> Position { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    public int SaveChanges();
}