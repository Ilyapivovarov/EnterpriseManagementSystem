namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record SignUp
    (string FirstName, string LastName, string Email, string Password, string ConfirmPassword) : ContractBase;