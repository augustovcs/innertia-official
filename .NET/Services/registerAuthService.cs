/*

Defined the packages and libraries for project usage below,
Keep just the "using" as already injected for better memory economy and usage.
For first time looking the project, keep maintain this arc model for Services.

*/



using BCrypt.Net;
using Auth.DTO;
using Auth.Interfaces;
using Auth.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;


namespace Auth.Services;


public class RegisterService : IRegisterService
{

    private readonly Supabase.Client _supabaseClient;

    public RegisterService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<bool> TestingRegUser(RegisterUserDTO credentials)
    {
        var post = await _supabaseClient
        .From<AuthCredentials>()
        .Where(a => a.Is_Admin == true)
        .Single();
        if (post == null)
        {
            return false; //se der nulo, significa que o email nao existe no banco
        }

        return true; //se der diferente de nulo, significa que o email existe, retorno true e acaba!


    }


    public async Task<bool> RegisterUser(RegisterUserDTO credentials)
    {
        
         
        
        var credentialPost = await _supabaseClient
        .From<AuthCredentials>()
        .Where(x => x.Email_Id == credentials.Email)
        .Single(); //tem que da nulo = significa que o email nao existe no banco

        if (credentialPost != null)
        {
            throw new ValidationException("Email already exists!"); //se der diferente de nulo, significa que o email existe, retorno false e acaba!

            //throw ou return, colocar o que fizer sentido ai (?)
        }


        if (string.IsNullOrEmpty(credentials.Password))
        {
            throw new ValidationException("Password cannot be empty.");
        }
        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(credentials.Password, 14);

        var newUser = new AuthCredentials // é igual um INSERT mas melhor
        {
            Email_Id = credentials.Email,
            Password_Id = hashedPassword,
            Is_Admin = false
        };

        if (string.IsNullOrEmpty(newUser.Email_Id)) {
            throw new ValidationException("Email cannot be empty.");
        }

        var response = await _supabaseClient
        .From<AuthCredentials>()
        .Insert(newUser);

        return response.Models != null && response.Models.Any();




    }




    
}