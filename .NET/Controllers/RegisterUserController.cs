/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for controllers.

*/

using Auth.DTO;
using Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class RegisterUserController : ControllerBase
{

    //This is our direct injection for the Interface that will handle the register services.
    public readonly IRegisterService _registerService;
    public RegisterUserController(IRegisterService registerService)
    {
        _registerService = registerService;

    }

    //This is a endpoint to register a user based with cryptography 13rd level
    //It is not meant for production use, but for testing purposes only.

    [HttpPost("register-user-auth")]
    public async Task<IActionResult> RegisterUser ([FromBody] RegisterUserDTO user)
    {
        var user_register = await _registerService.RegisterUser(user);

        if (!user_register)
        {
            return BadRequest("User already exists or registration failed.");
        }

        if (user.Email == string.Empty || user.Password == string.Empty)
        {
            
            return BadRequest("Email and Password cannot be empty.");
            
        }

        if (user_register == false)
        {
            return BadRequest("User registration failed.");
        }

        // Registration successful
        Console.WriteLine($"User registered: {user_register}");
        return Ok(new
        {
            message = "User registered successfully",
            user.Email,
            // user.Password // LOG TEST removed for security reasons, not for production use for evicting leaks
        });
    }

}
