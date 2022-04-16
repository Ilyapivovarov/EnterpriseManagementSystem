namespace EnterpriseManagementSystem.Contracts.WebContracts;

public record SignInDto(string Email, string Password);

public record SignUpDto(string Email, string Password, string ConfirmPassword);

public record UserDto(int Id, Guid Guid, string Email, string FirstName, string LastName, string Role);

public record SessionDto(string AccessToken, string RefreshToken, Guid UserGuid);