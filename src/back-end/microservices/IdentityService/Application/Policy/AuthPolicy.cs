using Microsoft.AspNetCore.Authorization;

namespace IdentityService.Application.Policy;

public static class ApplicationPolicy
{
    public static AuthorizationPolicy GetAuthorizationPolicy()
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        return policy;
    } 
}