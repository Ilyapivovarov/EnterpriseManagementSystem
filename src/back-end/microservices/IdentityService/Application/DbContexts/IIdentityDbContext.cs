using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.DbContexts;

public interface IIdentityDbContext
{
    public DbSet<UserDbEntity> Users { get; }

    public DbSet<SessionDbEntity> Sessions { get; }

    public DbSet<EmailAddressDbEntity> EmailAddresses { get; }

    public DbSet<UserRoleDbEntity> UserRoles { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}