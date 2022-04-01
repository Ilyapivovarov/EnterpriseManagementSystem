using System.Text.Json;
using Microsoft.AspNetCore.Mvc;


namespace ApiGateway.Infrastructure.HttpClientServices;

public class AuthHttpClientService : IAuthHttpClientService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public AuthHttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    
    public async Task<IActionResult> SignInAsync(SignInDto signIn)
    {
        var content = new StringContent(JsonSerializer.Serialize(signIn), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(UrlConfig.IdentityApi.SignIn, content);
        
        var sessionDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
           var sessionDto = JsonSerializer.Deserialize<SessionDto>(sessionDraft, _jsonSerializerOptions);
           return new OkObjectResult(sessionDto);
        } 
        
        return new BadRequestObjectResult(sessionDraft);
    }

    public async Task<IActionResult> SignUpAsync(SignUpDto signUpDto)
    {
        var content = new StringContent(JsonSerializer.Serialize(signUpDto), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(UrlConfig.IdentityApi.SignUp, content);
        
        var sessionDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var sessionDto = JsonSerializer.Deserialize<SessionDto>(sessionDraft, _jsonSerializerOptions);
            return new OkObjectResult(sessionDto);
        } 
        
        return new BadRequestObjectResult(sessionDraft); 
    }
}