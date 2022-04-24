using Microsoft.EntityFrameworkCore;

namespace EmailService.Application.Contexts;

public interface IEmailServiceDbContext
{
    public DbSet<UserDbEntity> Users { get; }
    
    public DbSet<EmailDbEntity> Emails { get; }
    
    public DbSet<EmailAddressDbEntity> EmailAddresses { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}