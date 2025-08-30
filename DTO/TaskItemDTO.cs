/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for DTOs .

*/

using System.Text.Json.Serialization;
using Task.Models;

namespace Task.DTO;

public class TaskItemEditDTO
{

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Status { get; set; }
    public int Priority { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
    public DateTime Finished_At { get; set; }


}

public class TaskItemDTO : TaskItemEditDTO

{
    
    public string StatusString => Status switch
    {
        0 => "Open",
        1 => "InProgress",
        2 => "Testing",
        3 => "Close",
        _ => "Open"
    };    
    
    public int User_ID { get; set; }
    public int Id { get; set; }

}