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
using Microsoft.AspNetCore.Identity;


namespace Auth.Services;


public class EditService : IEditService
{

    private readonly Supabase.Client _supabaseClient;

    public EditService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

   public async Task<bool> EditUser(AuthLoginDTO credentials)
{
    if (credentials.Id <= 0)
        throw new ValidationException("User ID cannot be 0 or negative.");

    var credentialPost = await _supabaseClient
        .From<AuthCredentials>()
        .Where(x => x.User_ID == credentials.Id)
        .Single();

    if (credentialPost == null)
        throw new ValidationException("User not found.");

    bool isEmailValid = !string.IsNullOrWhiteSpace(credentials.Email) && credentials.Email != "string";
    bool isPasswordValid = !string.IsNullOrWhiteSpace(credentials.Password) && credentials.Password != "string";

    if (!isEmailValid && !isPasswordValid)
        throw new ValidationException("Neither email nor password will be changed. Nothing was updated.");

    if (isEmailValid)
        credentialPost.Email_Id = credentials.Email;

    if (isPasswordValid)
        credentialPost.Password_Id = BCrypt.Net.BCrypt.HashPassword(credentials.Password);

    credentialPost.Last_Update = DateTime.UtcNow;

    var response = await _supabaseClient
        .From<AuthCredentials>()
        .Update(credentialPost);

    if (response.Models == null || !response.Models.Any())
        throw new ValidationException("User update failed.");

    return response.Models != null && response.Models.Any();
}
    
}