namespace EnterpriseManagementSystem.Contracts.WebContracts;

public record UpdatePasswordInfo(string Email, string NewPassword, string ConfirmNewPassword);