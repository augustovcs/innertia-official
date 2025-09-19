/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Services.

*/


using System.ComponentModel.DataAnnotations;
using Auth.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Supabase.Gotrue;
using Task.DTO;
using Analytics.Interfaces;
using Task.Models;

using Task.Models;

namespace Task.Services;

public class analyticsKanbanService : IAnalyticsKanban
{
    private Supabase.Client _supabaseClient;

    public analyticsKanbanService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<int> QuantityTasks(int status)
    {
        var taskPost = await _supabaseClient
            .From<TaskItem>()
            .Where(x => x.Status == status)
            .Get();

        
        
        return taskPost.Models.Count;

    }
                
}