/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for DTOs .

*/


using System.Text.Json.Serialization;


namespace Auth.DTO;

public class RegisterUserDTO
{

    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("password_hash")]
    public string Password { get; set; }

    // Constructor //
    
}