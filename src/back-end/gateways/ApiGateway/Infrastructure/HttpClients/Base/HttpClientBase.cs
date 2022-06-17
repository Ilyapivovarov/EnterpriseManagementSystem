namespace ApiGateway.Infrastructure.HttpClients.Base;

public abstract class HttpClientBase
{
    private readonly JsonSerializerOptions? _jsonSerializerOptions;

    protected HttpClientBase()
    {
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    protected StringContent GetStringContent(string content)
    {
        return new StringContent(content, Encoding.UTF8,
            MediaTypeNames.Application.Json);
    }

    protected T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
    }
}