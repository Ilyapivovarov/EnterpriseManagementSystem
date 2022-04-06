using IdentityService.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.AppData;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Session> Sessions => Set<Session>();
}