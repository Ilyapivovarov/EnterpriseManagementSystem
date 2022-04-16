namespace EnterpriseManagementSystem.Contracts.WebContracts;

public record SignIn(string Email, string Password);

public record SignUpDto(string Email, string Password, string ConfirmPassword);

public record Session(string AccessToken, string RefreshToken, Guid UserGuid);

public record Account(Guid Guid, string Email, string FirstName, string LastName, string Role);

public record UserInfo(Guid Guid, string? FirstName, string? LastName, string? Role);