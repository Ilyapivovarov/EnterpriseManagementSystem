using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.DbContexts;

public sealed class IdentityDbContext : DbContext, IIdentityDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();

    public DbSet<SessionDbEntity> Sessions => Set<SessionDbEntity>();

    public DbSet<EmailDbEntity> EmailAddresses => Set<EmailDbEntity>();

    public DbSet<UserRoleDbEntity> UserRoles => Set<UserRoleDbEntity>();
}