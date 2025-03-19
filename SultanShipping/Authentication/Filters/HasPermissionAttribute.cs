using Microsoft.AspNetCore.Authorization;

namespace SultanShipping.Authentication.Filters;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}