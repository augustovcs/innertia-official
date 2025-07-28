/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Services.

*/



using BCrypt.Net;
using Auth.DTO;
using Auth.Interfaces;
using Auth.Models;


namespace Auth.Services;


public class RegisterService : IRegisterService
{

    private readonly Supabase.Client _supabaseClient;

    public RegisterService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<bool> RegisterUser(RegisterUserDTO credentials)
    {

        var credentialPost = await _supabaseClient
        .From<AuthCredentials>()
        .Where(x => x.Email_Id == credentials.Email)
        .Single();

        if (credentialPost != null)
        {
            return false;
        }

        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(credentials.Password, 14);

        var newUser = new AuthCredentials
        {
            Email_Id = credentials.Email,
            Password_Id = hashedPassword,
            Is_Admin = false
        };


        var response = await _supabaseClient
        .From<AuthCredentials>()
        .Insert(newUser);

        return response.Models != null && response.Models.Any();
        



    }




    
}