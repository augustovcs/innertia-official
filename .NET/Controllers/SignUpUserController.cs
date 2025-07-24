/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for controllers.

*/

using Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class SignUpUserController : ControllerBase
{
    public readonly IAuthTesting _authTesting;


    public SignUpUserController(IAuthTesting authTesting)
    {
        _authTesting = authTesting;

    }

    [HttpGet("get-all-users-created")]
    public async Task<IActionResult> GetAllUsersCreated()
    {
        
        var user_credentials = await _authTesting.GetAllUserCredentials();
        return Ok(user_credentials);
        
    }

    [HttpPost("test-register")]
    public async Task<IActionResult> TestingRegister()
    {
        var users = 10;
        return Ok(users);
    }

}
