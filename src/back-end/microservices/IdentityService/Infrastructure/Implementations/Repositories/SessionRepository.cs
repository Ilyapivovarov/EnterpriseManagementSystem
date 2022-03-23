namespace IdentityService.Infrastructure.Implementations.Repositories;

public class SessionRepository : RepositoryBase, ISessionRepository
{
    public SessionRepository(ApplicationDbContext dbContext, ILogger logger) 
        : base(dbContext, logger)
    { }
    
    public bool SaveSession(Session session)
    {
        return WriteData(db => db.Sessions.Add(session),
            $"Error while save session for user with email {session.User.Email}");
    }

    public async Task<bool> SaveSessionAsync(Session session)
    {
        return await Task.Run(() => SaveSession(session));
    }
}