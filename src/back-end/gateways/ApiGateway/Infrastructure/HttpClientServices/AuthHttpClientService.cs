using ApiGateway.Application.ServiceInterfaces;
using ApiGateway.Dto;

namespace ApiGateway.Infrastructure.HttpClientServices;

public class AuthHttpClientService : IAuthHttpClientService
{
    private readonly HttpClient _httpClient;

    public AuthHttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<SessionDto> SignInAsync(SignInDto signIn)
    {
        throw new NotImplementedException();
    }

    public async Task<SessionDto> SignUpAsync(SignUpDto signUpDto)
    {
        throw new NotImplementedException();
    }
}