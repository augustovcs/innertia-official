/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Services.

*/


using Auth.DTO;
using Auth.Interfaces;
using Auth.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Auth.Services;


public class LoginUserService : IAuthService
{
    private readonly Supabase.Client _supabaseClient;

    public LoginUserService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<AuthCredentials> LoginUser(LoginUserDTO credentials)
    {
        /*
        essa verificação está bugada!
        ele nunca verifica apenas email ou senha, ele verifica se o email ta certo
        se nao tiver, ele ja da null
        se tiver, ele verifica a senha.

        */

        var credentialPost = await _supabaseClient
            .From<AuthCredentials>()
            .Where(c => c.Email_Id == credentials.Email)
            .Get();

            if (credentialPost == null || !credentialPost.Models.Any())
            {
            ///Console.WriteLine("Wrong email");
            return null;
            }

            var user = credentialPost.Models.First();
            

            bool verifyHashed = BCrypt.Net.BCrypt.EnhancedVerify(credentials.Password, user.Password_Id);

            if (!verifyHashed)
            {
            ///Console.WriteLine("Wrong password");
            return null;
            }

        /*
        LOG TESTING
        this is just for testing, not for production use for evicting leaks

        Console.WriteLine($"Password (hash): {user.Password_Id}");
        Console.WriteLine($"Password (crude): {credentials.Password}");

        */
        
            
            return user;

        
    }

}