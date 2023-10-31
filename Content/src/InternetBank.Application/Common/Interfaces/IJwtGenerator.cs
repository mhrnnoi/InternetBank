using InternetBank.Application.Authentication.Queries.Common;

namespace InternetBank.Application.Common.Interfaces;

public interface IJwtGenerator
{
    string GenerateToken(UserDTO userDTO);
}