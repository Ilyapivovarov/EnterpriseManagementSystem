using Microsoft.EntityFrameworkCore;

namespace EmailService.Application.Contexts;

public interface IEmailServiceDbContext
{
    public DbSet<AuthorDbEntity> Authors { get; }

    public DbSet<EmailDbEntity> Emails { get; }

    public DbSet<EmailAddressDbEntity> EmailAddresses { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}