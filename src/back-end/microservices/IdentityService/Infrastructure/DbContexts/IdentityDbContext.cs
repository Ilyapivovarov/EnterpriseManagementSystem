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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed(_securityService);
    }
}

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder, ISecurityService _securityService)
    {
        modelBuilder.Entity<EmailDbEntity>()
            .HasData(new EmailDbEntity
            {
                Id = 1,
                Address = "admin@admin.com",
                IsVerified = false
            });

        modelBuilder.Entity<UserRoleDbEntity>()
            .HasData(new UserRoleDbEntity
            {
                Id = 1,
                Name = DefaultUserRoleNames.Admin
            });

        modelBuilder.Entity<UserDbEntity>()
            .HasData(new UserDbEntity
            {
                Id = 1,
                Email = new EmailDbEntity
                {
                    Id = 1,
                    Address = "admin@admin.com",
                    IsVerified = false
                },
                Password = _securityService.EncryptPasswordOrException("admin"),
                Role = new UserRoleDbEntity
                {
                    Id = 1,
                    Name = DefaultUserRoleNames.Admin
                }
            });
    }
}