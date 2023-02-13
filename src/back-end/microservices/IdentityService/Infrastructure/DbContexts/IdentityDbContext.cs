using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.DbContexts;

public sealed class IdentityDbContext : DbContext, IIdentityDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    { }

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();

    public DbSet<EmailDbEntity> EmailAddresses => Set<EmailDbEntity>();

    public DbSet<UserRoleDbEntity> UserRoles => Set<UserRoleDbEntity>();

    public async Task<bool> CanConnectAsync()
    {
        return await Database.CanConnectAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<EmailDbEntity>()
            .Property(entity => entity.Address)
            .HasConversion(
                property => property.Value,
                value => EmailAddress.Parse(value));

        modelBuilder
            .Entity<UserDbEntity>()
            .Property(entity => entity.Password)
            .HasConversion(
                property => property.Value,
                value => Password.Parse(value));
    }
}