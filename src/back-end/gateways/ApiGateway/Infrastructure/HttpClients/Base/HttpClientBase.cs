using System.Net;

namespace ApiGateway.Infrastructure.HttpClients.Base;

public abstract class HttpClientBase
{
    protected static StringContent GetStringContent(string content)
    {
        return new(content, Encoding.UTF8, MediaTypeNames.Application.Json);
    }

    protected static IActionResult GetActionResult(object obj, HttpStatusCode statusCode)
    {
        return new StatusCodeResult((int) statusCode);
    }

    protected static IActionResult GetObjectActionResult(object obj, HttpStatusCode statusCode)
    {
        return new OkObjectResult(obj) {StatusCode = (int) statusCode};
    }
}