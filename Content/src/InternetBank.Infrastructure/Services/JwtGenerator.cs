using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InternetBank.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InternetBank.Infrastructure.Services;

public class JwtGenerator : IJwtGenerator
{
    // public JwtGenerator(IConfiguration configuration)
    // {
    //     Configuration = configuration;
    // }

    // public IConfiguration Configuration { get; }

    public string GenerateToken()
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "subclaim"),
            new Claim(JwtRegisteredClaimNames.Acr, "acrclaim"),
            new Claim(JwtRegisteredClaimNames.FamilyName, "noi"),
        };

        var cred = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key")), SecurityAlgorithms.HmacSha512);
        var securityToken = new JwtSecurityToken(issuer: "mehran", audience: "user", claims, signingCredentials: cred, expires: DateTime.Now.AddMinutes(5));
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(securityToken);
    }
}
