using Microsoft.EntityFrameworkCore;

namespace EmailService.Infrastructure.Contexts;

public sealed class EmailServiceDbContext : DbContext, IEmailServiceDbContext
{
    public EmailServiceDbContext(DbContextOptions<EmailServiceDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    #region Implement IEmailServiceDbContext

    public DbSet<AuthorDbEntity> Authors => Set<AuthorDbEntity>();

    public DbSet<EmailDbEntity> Emails => Set<EmailDbEntity>();

    public DbSet<EmailAddressDbEntity> EmailAddresses => Set<EmailAddressDbEntity>();

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    #endregion
}