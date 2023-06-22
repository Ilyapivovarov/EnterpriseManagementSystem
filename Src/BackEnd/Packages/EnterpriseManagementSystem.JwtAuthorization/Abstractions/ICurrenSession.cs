namespace EnterpriseManagementSystem.JwtAuthorization.Abstractions;

public interface ICurrenSession
{
    public ICurrentUser? CurrentUser { get; set; }
}

public class CurrenSession : ICurrenSession
{
    public ICurrentUser? CurrentUser { get; set; }
}

public interface ICurrentUser
{
    public string Email { get; }
    
    public Guid Guid { get; set; }

    public string Role { get; set; }
}

public class CurrentUser : ICurrentUser
{
    public CurrentUser(Guid guid,  string role, string email)
    {
        Guid = guid;
        Role = role;
        Email = email;
    }
    
    public Guid Guid { get; set; }
    
    public string Role { get; set; }
    
    public string Email { get; }
}