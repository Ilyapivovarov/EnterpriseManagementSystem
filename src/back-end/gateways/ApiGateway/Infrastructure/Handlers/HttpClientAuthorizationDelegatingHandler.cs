using Microsoft.Net.Http.Headers;

namespace ApiGateway.Infrastructure.Handlers;

public class HttpClientAuthorizationDelegatingHandler
    : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext
                .Request.Headers[HeaderNames.Authorization];

            if (!string.IsNullOrWhiteSpace(authorizationHeader))
            {
                request.Headers.Add(HeaderNames.Authorization, authorizationHeader.ToArray());
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}