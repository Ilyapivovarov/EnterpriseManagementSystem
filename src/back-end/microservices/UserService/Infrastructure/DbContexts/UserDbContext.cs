using UserService.Application.DbContexts;
using UserService.Core.DbEntities;

namespace UserService.Infrastructure.DbContexts;

public sealed class UserDbContext : DbContext, IUserDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();

    public DbSet<EmployeeDbEntity> Eployees => Set<EmployeeDbEntity>();

    public DbSet<PositionDbEntity> Position => Set<PositionDbEntity>();
}