using UserService.Core.DbEntities.Base;

namespace UserService.Infrastructure.DbContexts;

public sealed class UserDbContext : DbContext, IUserDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
        SavingChanges += UpdateState;
    }

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();

    public DbSet<EmployeeDbEntity> Eployees => Set<EmployeeDbEntity>();

    public DbSet<PositionDbEntity> Positions => Set<PositionDbEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbEntityBase>().HasQueryFilter(entity => !entity.IsDeleted);
        base.OnModelCreating(modelBuilder);
    }

    private void UpdateState(object? sender, SavingChangesEventArgs e)
    {
        foreach (var entry in ChangeTracker.Entries<DbEntityBase>())
            switch (entry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
                case EntityState.Modified:
                    entry.Entity.Updated = DateTime.Now;
                    break;
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.Updated = DateTime.Now;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }
}