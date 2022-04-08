namespace EnterpriseManagementSystem.Contracts.WebContracts;

public record SignInDto(string Email, string Password);

public record SignUpDto(string Email, string Password, string ConfirmPassword);

public record UserDto(int Id, string Email, Guid Guid);

public record SessionDto(string AccessToken, string RefreshToken, Guid UserGuid);