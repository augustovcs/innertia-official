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
[Route("api/edit")]
public class EditUserController : ControllerBase
{

    //This is our direct injection for the Interface that will handle the register services.
    public readonly IEditService _editService;
    public EditUserController(IEditService editService)
    {
        _editService = editService;

    }

    //This is a endpoint to edit a user based -> working with just email and password
    //It is not meant for production use, but for testing purposes only.

    [HttpPost("edit-user")]
    public async Task<IActionResult> EditUser([FromBody] AuthLoginDTO user)
    {
        var user_edit = await _editService.EditUser(user);

        if (!user_edit)
        {
            return BadRequest("User does not exist or edit failed.");
        }

        if (user.Email == string.Empty || user.Password == string.Empty)
        {
            return BadRequest("Email and Password cannot be empty.");
        }

        if (user_edit == false)
        {
            return BadRequest("User edit failed.");
        }

        Console.WriteLine($"User edited: {user_edit}");
        return Ok(new
        {
            message = "User edited successfully",
            user.Email,
            //user.Password // LOG TEST removed for security reasons, not for production use for evicting leaks
        });
     
    }

}
