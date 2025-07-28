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

        // Registration successful
        Console.WriteLine($"User registered: {user_register}");
        return Ok(new
        {
            message = "User registered successfully",
            user.Email,
            user.Password //remover em breve, para evitar endpoint soltando senha crua
        });
    }

}
