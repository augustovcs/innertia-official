/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for controllers.

*/

using Task.DTO;
using Task.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Task.Controllers;

[ApiController]
[Route("api/auth")]
public class TaskItemController : ControllerBase
{

    //This is our direct injection for the Interface that will handle the register services.
    public readonly ITaskItem _taskService;
    public TaskItemController(ITaskItem taskService)
    {
        _taskService = taskService;

    }

    //This is a endpoint to register a user based with cryptography 13rd level
    //It is not meant for production use, but for testing purposes only.

    [HttpPost("add/task")]
    public async Task<IActionResult> AddTaskItem([FromBody] TaskItemDTO task)
    {
        var task_post = await _taskService.AddTask(task);

        if (!task_post)
        {
            return BadRequest("Task failed.");
        }

        if (task.User_ID < 1)
        {

            return BadRequest("Email cannot be empty.");

        }

        if (task_post == false)
        {
            return BadRequest("Was not possible to add this Task!");
        }

        // Task added successful
        Console.WriteLine($"Task added: {task_post}");
        return Ok(new
        {
            message = "Task added successfully",
            task.User_ID,
            task.Title,
            task.Description

        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveTask(int id)
    {
        var delete_task = await _taskService.RemoveTask(new TaskItemDTO {Id = id });

        if (!delete_task)
        {
            return NotFound();
        }

        return NoContent();
    }
 

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {

        var tasks = await _taskService.GetAllTasks();
        return Ok(tasks);
    }

}
