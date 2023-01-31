using EnterpriseManagementSystem.JwtAuthorization.Models;

namespace EnterpriseManagementSystem.JwtAuthorization.Interfaces;

public interface IJwtSession
{
    public JwtToken AccessToken { get; set; }
    
    public JwtToken RefreshToken { get; set; }
}