/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Services.

*/


using System.ComponentModel.DataAnnotations;
using Auth.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Supabase.Gotrue;
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
            User_ID = c.User_ID,
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
        .Where(x => x.User_ID == task.User_ID)
        .Single();

        if (credentialPost == null)
        {
            throw new ValidationException("User not found! please try again");
        }

        var userResponse = await _supabaseClient
        .From<AuthCredentials>()
        .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, task.User_ID)
        .Single();

        if (userResponse == null)
        {
            throw new ValidationException("User not found! ");

        }



        var newTask = new TaskItem
        {

            User_ID = task.User_ID,
            User_Email = userResponse.Email_Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            Created_At = DateTime.Now,



        };

        var response = await _supabaseClient
        .From<TaskItem>()
        .Insert(newTask);
      

        return response.Models != null && response.Models.Any();

    }

    public async Task<bool> RemoveTask(TaskItemDTO task)
    {

        try
        {
            await _supabaseClient
            .From<TaskItem>()
            .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, task.Id)
            .Delete();

            return true;
        }

        catch
        {
            return false;
        }

        

    }


    public async Task<bool> EditTask(TaskItemDTO task)
    {

        if (task.Id <= 0)
        {
            throw new ValidationException("Id cannot be 0 or less");
        }

        var editPost = await _supabaseClient
        .From<TaskItem>()
        .Where(x => x.Task_ID == task.Id)
        .Single();

        if (editPost == null)
        {
            throw new ValidationException("Id not found!");

        }

        editPost.Updated_At = DateTime.UtcNow;

        var response = await _supabaseClient
        .From<TaskItem>()
        .Update(editPost);

        if (response.Models == null || !response.Models.Any())
        {
            throw new ValidationException("Task update failed! ");


        }

        return response.Models != null && response.Models.Any();


    }


}