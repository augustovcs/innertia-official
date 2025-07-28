/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for controllers.

*/

using Auth.DTO;
using Auth.Interfaces;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;


namespace Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class SignUpUserController : ControllerBase
{

    //This is our direct injection for the Interface that will handle the testing services.
    public readonly ITestingServices _authTesting;
    public SignUpUserController(ITestingServices authTesting)
    {
        _authTesting = authTesting;

    }

    //This is a testing endpoint to get all users created
    //It is not meant for production use, but for testing purposes only.

    [HttpGet("get-all-users-created")]
    public async Task<IActionResult> GetAllUsersCreated()
    {

        var user_credentials = await _authTesting.GetAllUserCredentials();
        return Ok(user_credentials);

    }

    //This is a testing endpoint to register a user
    //It is not meant for production use, but for testing purposes only.

    [HttpPost("test-register")]
    public async Task<IActionResult> TestingRegister([FromBody] RegisterUserDTO user)
    {
        var user_register = await _authTesting.RegisterUserTesting(user);

        if (!user_register)
        {
            return BadRequest("User already exists or registration failed.");
        }

        // Registration successful
        Console.WriteLine($"User registered: {user_register}");
        return Ok(new
        {
            message = "User registered successfully",
            user.Email,
            user.Password
        });
    }

    [HttpPost("test-login")]
    public async Task<IActionResult> TestingLogin([FromBody] RegisterUserDTO user)
    {
        var user_login = await _authTesting.LoginUserTesting(user);

        if (!user_login)
        {
            return BadRequest("User doesn't exist or login failed.");
        }
        
        Console.WriteLine($"User logged: {user_login}");

        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 14);

        return Ok(new
        {
            message = "User logged successfully",
            user.Email,
            hashedPassword
        });

    }

}
