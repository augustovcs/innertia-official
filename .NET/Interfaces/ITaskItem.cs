/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Interfaces.

*/


using Auth.DTO;
using Auth.Models;
using Task.Models;

namespace Task.Interfaces;

public interface ITaskItem
{
    Task<List<TaskItemDTO>> GetAllTasks(TaskItemDTO task);

    Task<TaskItem> AddTask_Testing(TaskItemDTO task);
    
}