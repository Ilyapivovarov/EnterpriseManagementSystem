using IdentityService.Infrastructure.AppData.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.AppData;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<User> Users => Set<User>();
}