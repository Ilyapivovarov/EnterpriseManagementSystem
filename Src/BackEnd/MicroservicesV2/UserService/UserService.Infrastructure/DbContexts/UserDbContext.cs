using Microsoft.EntityFrameworkCore;
using UserService.Core.DbEntities.Base;

namespace UserService.Infrastructure.DbContexts;

public sealed class UserDbContext : DbContext, IUserDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
        SavingChanges += UpdateState;
    }

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();

    public DbSet<EmployeeDbEntity> Employees => Set<EmployeeDbEntity>();

    public DbSet<PositionDbEntity> Positions => Set<PositionDbEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDbEntity>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<EmployeeDbEntity>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<PositionDbEntity>().HasQueryFilter(entity => !entity.IsDeleted);
        
        modelBuilder
            .Entity<UserDbEntity>()
            .Property(entity => entity.EmailAddress)
            .HasConversion(
                property => property.Value,
                value => EmailAddress.Parse(value));
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