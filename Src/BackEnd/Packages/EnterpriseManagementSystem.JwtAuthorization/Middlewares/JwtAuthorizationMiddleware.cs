using EnterpriseManagementSystem.JwtAuthorization.Abstractions;
using EnterpriseManagementSystem.JwtAuthorization.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseManagementSystem.JwtAuthorization.Middlewares;

public class JwtAuthorizationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.Request.Headers.Authorization.IsNullOrEmpty())
        {
            var currenSession = context.RequestServices.GetRequiredService<ICurrenSession>();
            
            var guid = Guid.Parse(context.User.Claims.Single(x => x.Properties.Values.Contains(EmsJwtClaimNames.Guid)).Value);
            var role = context.User.Claims.Single(x =>  x.Properties.Values.Contains(EmsJwtClaimNames.Role)).Value;
            var email = context.User.Claims.Single(x => x.Properties.Values.Contains(EmsJwtClaimNames.Email)).Value;

            currenSession.CurrentUser = new CurrentUser(guid, role, email);
        }
        
        await next(context);
    }
}
