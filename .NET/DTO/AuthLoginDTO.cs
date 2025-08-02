/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for DTOs .

*/

using System.Text.Json.Serialization;

namespace Auth.DTO;


public class AuthLoginDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; } = null!;
    [JsonPropertyName("password_hash")]
    public string? Password { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime updated_at { get; set; }


}





