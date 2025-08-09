/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for DTOs .

*/

using System.Text.Json.Serialization;

namespace Task.DTO;

public class TaskItemDTO
{
    public int Id { get; set; }
    public int User_ID { get; set; }
    [JsonIgnore]
    public string Email { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Status { get; set; }
    public int Priority { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
    public DateTime Finished_At { get; set; }

    
}