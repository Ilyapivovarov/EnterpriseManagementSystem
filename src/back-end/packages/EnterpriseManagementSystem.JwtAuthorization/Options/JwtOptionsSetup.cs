using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EnterpriseManagementSystem.JwtAuthorization.Options;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string JwtSectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(JwtOptions options)
    {
        _configuration.GetRequiredSection(JwtSectionName).Bind(options);
    }
}