using Auth.DTO;
using Auth.Interfaces;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class LoginUserController : ControllerBase
{
    private readonly IAuthService _authTesting;

    public LoginUserController(IAuthService authTesting)
    {
        _authTesting = authTesting;
    }

    [HttpPost("login-user")]
    public async Task<IActionResult> Authentication([FromBody] LoginUserDTO user)
    {
        AuthCredentials loginSuccess = await _authTesting.LoginUser(user);

         if (loginSuccess == null)
        {
            return BadRequest("Email or password incorrect. ");
        }

        Console.WriteLine($"User logged: {user.Email}");

        return Ok(new
        {
            message = "User logged successfully",
            email = user.Email
        });
    }
}
