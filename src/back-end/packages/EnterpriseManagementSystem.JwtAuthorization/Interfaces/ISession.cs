namespace EnterpriseManagementSystem.JwtAuthorization.Interfaces;

public interface IJwtSession
{
    public string AccessToken { get; set; }
    
    public string RefreshToken { get; set; }
}