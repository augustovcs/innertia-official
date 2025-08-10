using Auth.DTO;
using Auth.Interfaces;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Configs.JwtRules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class LoginUserController : ControllerBase
{
    private readonly IAuthService _authTesting;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public LoginUserController(IAuthService authTesting)
    {
        _authTesting = authTesting;
    }

    public LoginUserController(JwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;

    }

    [HttpPost("login")]
    public async Task<IActionResult> Authentication([FromBody] LoginUserDTO_FULL user)
    {
        AuthCredentials loginSuccess = await _authTesting.LoginUser(user);

         if (loginSuccess == null)
        {
            return BadRequest("Email or password incorrect. ");
        }

        Console.WriteLine($"User logged: {user.Email}");

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);


        return Ok(new
        {
            token,
            user = new { user.Id, user.Email},

            message = "User logged successfully",
            email = user.Email
        });
    }
}
