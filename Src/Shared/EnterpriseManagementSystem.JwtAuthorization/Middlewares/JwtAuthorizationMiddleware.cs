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
        if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
        {
            var currenSession = context.RequestServices.GetRequiredService<ICurrenSession>();
            
            var guid = Guid.Parse(context.User.Claims.Single(x => x.Type == EmsJwtClaimNames.Guid).Value);
            var role = context.User.Claims.Single(x =>  x.Type == EmsJwtClaimNames.Role).Value;
            var email = context.User.Claims.Single(x => x.Type == EmsJwtClaimNames.Email).Value;
        
            currenSession.CurrentUser = new CurrentUser(guid, role, email);
        }
        
        await next(context);
    }
}