/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for controllers.

*/


using Auth.DTO;
using Auth.Interfaces;
using Auth.Models;

namespace Auth.Services;

public class AuthTestingService : IAuthTesting
{
    private readonly Supabase.Client _supabaseClient;

    public AuthTestingService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<List<AuthLoginDTO>> GetAllUserCredentials()
    {
        var credentialsResponse = await _supabaseClient
        .From<AuthCredentials>()
        .Limit(10000)
        .Get();

        Console.WriteLine($"Total found: {credentialsResponse.Models.Count}");


        return credentialsResponse.Models.Select(c => new AuthLoginDTO
        {

            Email = c.Email_Id,
            Password = c.Password_Id
        }).ToList();
        
    }

}