using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SultanShipping.Abstractions;
using SultanShipping.Contracts.Users;
using SultanShipping.Extensions;
using SultanShipping.UserServices.Services;

namespace SultanShipping.Controllers;

[Route("me")]
[Authorize]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("")]
    public async Task<IActionResult> Info()
    {
        var result = await _userService.GetProfileAsync(User.GetUserId()!);

        return Ok(result.Value);
    }

    //[HttpPut("info")]
    [HttpPost("info")]
    public async Task<IActionResult> Info([FromBody] UpdateProfileRequest request)
    {
        await _userService.UpdateProfileAsync(User.GetUserId()!, request);

        return NoContent();
    }

    //[HttpPut("change-password")]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await _userService.ChangePasswordAsync(User.GetUserId()!, request);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}