namespace SultanShipping.Contracts.Users;

public record UpdateProfileRequest(
    string FirstName,
    string LastName
);