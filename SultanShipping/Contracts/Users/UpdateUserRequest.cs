﻿namespace SultanShipping.Contracts.Users;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    IList<string> Roles,
    string phone,
    string ShippingAddress
);