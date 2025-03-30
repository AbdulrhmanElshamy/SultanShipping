namespace SultanShipping.Contracts.Users;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ShippingAddress,
    string Phone
);