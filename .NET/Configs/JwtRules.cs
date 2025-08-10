using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Configs.JwtRules;


public class JwtTokenGenerator
{
    private readonly IConfiguration _config;


    public JwtTokenGenerator(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(int id, string email)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );

        var credentials = new SigningCredentials(securityKey,
        SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim("userId", id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials


        );


        return new JwtSecurityTokenHandler().WriteToken(token);

    }


}


