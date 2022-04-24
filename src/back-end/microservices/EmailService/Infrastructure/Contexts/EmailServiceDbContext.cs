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

    public DbSet<UserDbEntity> Users => Set<UserDbEntity>();
    
    public DbSet<EmailDbEntity> Emails => Set<EmailDbEntity>();
    
    public DbSet<EmailAddressDbEntity> EmailAddresses  => Set<EmailAddressDbEntity>();

    #endregion
}