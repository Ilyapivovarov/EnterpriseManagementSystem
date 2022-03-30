using IdentityService.Application.RepositoryInterfaces;

namespace IdentityService.Infrastructure.Implementations.Repositories;

public class SessionRepository : RepositoryBase, ISessionRepository
{
    public SessionRepository(ApplicationDbContext dbContext,  ILogger<SessionRepository> logger) 
        : base(dbContext, logger)
    { }
    
    public bool SaveSession(Session session)
    {
        return WriteData(db =>
            {
                var activeSession = db.Sessions.FirstOrDefault(x => x.User.Id == session.User.Id);
                if (activeSession != null)
                {
                    activeSession.AccessToken = session.AccessToken;
                    activeSession.RefreshToken = session.RefreshToken;
                    
                    db.Sessions.Update(activeSession);
                }
                else
                {
                    db.Sessions.Add(session);
                }

                
            },
            $"Error while save session for user with email {session.User.Email}");
    }

    public async Task<bool> SaveSessionAsync(Session session)
    {
        return await Task.Run(() => SaveSession(session));
    }
}