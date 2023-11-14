using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InternetBank.Infrastructure.Services;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(UserDTO userDTO)
    {


        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userDTO.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, userDTO.Email),
            new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer),
            new Claim(JwtRegisteredClaimNames.Exp, _jwtSettings.Expiry.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, userDTO.FirstName + " " + userDTO.LastName),
        };

        var cred = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                                          SecurityAlgorithms.HmacSha512);

        var securityToken = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                                                 audience: _jwtSettings.Audience,
                                                 claims,
                                                 signingCredentials: cred,
                                                 expires: DateTime.Now.AddMinutes(_jwtSettings.Expiry));

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(securityToken);
    }
}
