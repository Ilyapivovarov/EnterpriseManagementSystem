using System.Net.Mime;
using System.Text;
using System.Text.Json;
using ApiGateway.Application.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Infrastructure.HttpClients;

public class AuthHttpClient : IAuthHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public AuthHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    
    public async Task<IActionResult> SignInAsync(SignInDto signIn)
    {
        var content = new StringContent(JsonSerializer.Serialize(signIn), Encoding.UTF8, MediaTypeNames.Application.Json);
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
        var content = new StringContent(JsonSerializer.Serialize(signUpDto), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync(UrlConfig.IdentityApi.SignUp, content);
        var sessionDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var sessionDto = JsonSerializer.Deserialize<SessionDto>(sessionDraft, _jsonSerializerOptions);
            return new OkObjectResult(sessionDto);
        } 
        
        return new BadRequestObjectResult(sessionDraft); 
    }

    public async Task<IActionResult> SignOutUser()
    {
        var response = await _httpClient.DeleteAsync(UrlConfig.IdentityApi.SignIn);
        
        var sessionDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var sessionDto = JsonSerializer.Deserialize<SessionDto>(sessionDraft, _jsonSerializerOptions);
            return new OkObjectResult(sessionDto);
        } 
        
        return new BadRequestObjectResult(sessionDraft);
    }
}