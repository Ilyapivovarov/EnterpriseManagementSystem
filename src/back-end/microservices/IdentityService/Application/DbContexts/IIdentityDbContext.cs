using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.DbContexts;

public interface IIdentityDbContext : IContext
{
    public DbSet<UserDbEntity> Users { get; }

    public DbSet<SessionDbEntity> Sessions { get; }

    public DbSet<EmailDbEntity> EmailAddresses { get; }

    public DbSet<UserRoleDbEntity> UserRoles { get; }
}

public interface IContext
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    public int SaveChanges();
}