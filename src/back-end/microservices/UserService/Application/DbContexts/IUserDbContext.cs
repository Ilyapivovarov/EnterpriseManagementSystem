using UserService.Core.DbEntities;

namespace UserService.Application.DbContexts;

public interface IUserDbContext
{
    public DbSet<UserDbEntity> Users { get; }

    public DbSet<EployeeDbEntity> Eployees { get; }

    public DbSet<PositionDbEntity> Position { get; }

    public Task<int> SaveChagesAsync(CancellationToken cancellationToken = default);

    public int SaveChanges();
}