/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Services.

*/


using System.ComponentModel.DataAnnotations;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Task.DTO;
using Task.Interfaces;
using Task.Models;



namespace Task.Services;

public class ItemTaskService : ITaskItem
{

    private readonly Supabase.Client _supabaseClient;
    public ItemTaskService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<List<TaskItemDTO>> GetAllTasks()
    {
        var taskPost = await _supabaseClient
        .From<TaskItem>()
        .Get();

        return taskPost.Models.Select(c => new TaskItemDTO
        {
            Id = c.Task_ID,
            Email = c.User_Email,
            Title = c.Title,
            Description = c.Description,
            Status = c.Status,
            Created_At = c.Created_At            
        }).ToList();
    }

    public async Task<bool> AddTask(TaskItemDTO task)
    {

        var credentialPost = await _supabaseClient
        .From<AuthCredentials>()
        .Where(x => x.Email_Id == task.Email)
        .Single();

        if (credentialPost == null)
        {
            throw new ValidationException("User not found! please try again");
        }

        var newTask = new TaskItem
        {
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            User_Email = task.Email,
            Created_At = DateTime.Now,



        };

        var response = await _supabaseClient
        .From<TaskItem>()
        .Insert(newTask);

        return response.Models != null && response.Models.Any();
 


        
    }


}