/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Models.

*/
using System.Data;
using Microsoft.AspNetCore.SignalR;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Task.Models;

[Table("task_item")]
public class TaskItem : BaseModel
{
    [PrimaryKey("id", false)]
    public int Task_ID { get; set; }

    [Column("title")]
    public string Title { get; set; } = null!;
    [Column("description")]
    public string Description { get; set; } = null!;
    [Column("priority")]
    public int Priority { get; set; }
    [Column("status")]
    public int Status { get; set; }

    // Foreign Key column below

    [Column("email")]
    public string User_Email { get; set; } = null!;

    // DateTime Columns

    [Column("created_at")]
    public DateTime Created_At { get; set; }
    
    [Column("updated_at")]
    public DateTime Updated_At { get; set; }

    [Column("finished_at")]
    public DateTime Finished_At { get; set; }


}