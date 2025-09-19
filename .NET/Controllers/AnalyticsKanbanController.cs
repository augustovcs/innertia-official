/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for controllers.

*/

using Analytics.Interfaces;
using Task.DTO;
using Task.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Supabase.Gotrue;

namespace Analytics.Controllers;

[ApiController]
[Route("api/analytics/kanban")]



public class AnalyticsKanbanController : ControllerBase
{
    public readonly IAnalyticsKanban _analyticsKanban;

    public AnalyticsKanbanController(IAnalyticsKanban analyticsKanban)
    {
        _analyticsKanban = analyticsKanban;
    }

    [HttpPatch("quantity/tasks/{status}")]
    public async Task<IActionResult> GetQuantityTasks(int status)
    {
        var task_get = await _analyticsKanban.QuantityTasks(status);

        
        
        return Ok($"A quantidade de tasks nesse status {status} é " + task_get);


    }
    
    
}