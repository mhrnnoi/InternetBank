using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InternetBank.Application.Authentication.Commands.Login;
using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InternetBank.Infrastructure.Services;

public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration configuration;

    public JwtGenerator(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GenerateToken(UserDTO userDTO)
    {

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userDTO.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, userDTO.Email),
            new Claim(JwtRegisteredClaimNames.Iss, configuration["JwtSettings:Issuer"]),
            // new Claim(JwtRegisteredClaimNames.Aud, configuration["JwtSettings:Audience"]),
            new Claim(JwtRegisteredClaimNames.Exp, configuration["JwtSettings:Expiry"]),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, userDTO.FirstName + " " + userDTO.LastName),
        };

        var cred = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"])), SecurityAlgorithms.HmacSha512);
        var securityToken = new JwtSecurityToken(issuer: configuration["JwtSettings:Issuer"], audience: configuration["JwtSettings:Audience"], claims, signingCredentials: cred, expires: DateTime.Now.AddMinutes(5));
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(securityToken);
    }
}
