using Microsoft.EntityFrameworkCore;
using UserService.Core;

namespace IdentityService.Infrastructure.DbContexts;

public sealed class IdentityDbContext : DbContext, IIdentityDbContext
{
    private readonly ISecurityService _securityService;

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, ISecurityService securityService)
        : base(options)
    {
        _securityService = securityService;
        Database.EnsureCreated();
    }

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();

    public DbSet<SessionDbEntity> Sessions => Set<SessionDbEntity>();

    public DbSet<EmailDbEntity> EmailAddresses => Set<EmailDbEntity>();

    public DbSet<UserRoleDbEntity> UserRoles => Set<UserRoleDbEntity>();
    
}