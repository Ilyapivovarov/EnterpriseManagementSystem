using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace EnterpriseManagementSystem.JwtAuthorization.Middlewares;

public class JwtSessionMiddleware 
{
    private readonly RequestDelegate _next;

    public JwtSessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
 
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.Authorization.IsNullOrEmpty() )
        {
            var currenSession = context.RequestServices.GetRequiredService<ICurrenSession>();

            var guid = Guid.Parse(context.User.Claims.Single(x => x.Properties.Values.Contains(JwtRegisteredClaimNames.Sub)).Value);
            var role = context.User.Claims.Single(x =>  x.Properties.Values.Contains("role")).Value;
            var email = context.User.Claims.Single(x => x.Properties.Values.Contains(JwtRegisteredClaimNames.Email)).Value;

            currenSession.CurrentUser = new CurrentUser(guid, role, email);
        }
        
        await _next(context);
    }
}