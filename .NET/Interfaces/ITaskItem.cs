/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Interfaces.

*/


using Task.DTO;
using Task.Models;

namespace Task.Interfaces;

public interface ITaskItem
{
    Task<List<TaskItemDTO>> GetAllTasks();
    Task<List<TaskItem>> GetTaskByStatus(int status);

    Task<bool> AddTask(TaskItemDTO task);
    Task<bool> RemoveTask(TaskItemDTO task);
    Task<bool> EditTask(int id, TaskItemEditDTO task);
    
}