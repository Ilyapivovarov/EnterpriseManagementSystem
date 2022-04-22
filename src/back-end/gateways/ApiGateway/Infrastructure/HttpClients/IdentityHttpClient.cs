namespace ApiGateway.Infrastructure.HttpClients.Identity;

public sealed class IdentityHttpClient : IIdentityHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public IdentityHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<IActionResult> SignInAsync(SignIn signIn)
    {
        var content = new StringContent(JsonSerializer.Serialize(signIn), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync(UrlConfig.IdentityApi.AuthController.SignIn(), content);

        var sessionDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var sessionDto = JsonSerializer.Deserialize<Session>(sessionDraft, _jsonSerializerOptions);
            return new OkObjectResult(sessionDto);
        }

        return new BadRequestObjectResult(sessionDraft);
    }

    public async Task<IActionResult> SignUpAsync(SignUp signUp)
    {
        var content = new StringContent(JsonSerializer.Serialize(signUp), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync(UrlConfig.IdentityApi.AuthController.SignUp(), content);
        var sessionDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var sessionDto = JsonSerializer.Deserialize<Session>(sessionDraft, _jsonSerializerOptions);
            return new OkObjectResult(sessionDto);
        }

        return new BadRequestObjectResult(sessionDraft);
    }

    public async Task<IActionResult> SignOutUser()
    {
        var response = await _httpClient.DeleteAsync(UrlConfig.IdentityApi.AuthController.SignOut());

        var sessionDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var sessionDto = JsonSerializer.Deserialize<Session>(sessionDraft, _jsonSerializerOptions);
            return new OkObjectResult(sessionDto);
        }

        return new BadRequestObjectResult(sessionDraft);
    }
    
    public async Task<IActionResult> GetAllUsers(int page = 0)
    {
        var response = await _httpClient.GetAsync(UrlConfig.IdentityApi.UserController.GetAllUser(page));

        var responseDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var accounts = JsonSerializer.Deserialize<Account[]>(responseDraft, _jsonSerializerOptions);
            return new OkObjectResult(accounts);
        }

        return new BadRequestObjectResult(responseDraft);
    }

    public async Task<IActionResult> GetUserByGuid(Guid userGuid)
    {
        var response = await _httpClient.GetAsync(UrlConfig.IdentityApi.UserController.GetUserByGuid(userGuid));

        var responseDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var sessionDto = JsonSerializer.Deserialize<Account>(responseDraft, _jsonSerializerOptions);
            return new OkObjectResult(sessionDto);
        }

        return new BadRequestObjectResult(responseDraft);
    }

    public async Task<IActionResult> UpdateUserData(UserInfo? userInfo)
    {
        var content = new StringContent(JsonSerializer.Serialize(userInfo), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var response = await _httpClient.PutAsync(UrlConfig.IdentityApi.UserController.UpdateUserData(), content);

        var responseDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return new OkResult();
        }

        return new BadRequestObjectResult(responseDraft);
    }
}