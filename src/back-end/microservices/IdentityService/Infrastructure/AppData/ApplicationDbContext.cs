using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.AppData;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();

    public DbSet<SessionDbEntity> Sessions => Set<SessionDbEntity>();
}