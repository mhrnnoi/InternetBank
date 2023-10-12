using InternetBank.Application.Features.Authentication.Commands.Common;

namespace InternetBank.Application.Common.Services;

public class AuthResultService
{
    public static RegisterActionResult CreateRegisterResult(string id,
                                                           string token)
    {
        return new RegisterActionResult("registeration succesfull",
                                       id,
                                       $"User/id={id}",
                                       token);
    }
    public static LoginActionResult CreateLoginResult(string id,
                                                           string token,
                                                           string username)
    {
        return new LoginActionResult("Login succesfull",
                                       id,
                                       username,
                                       token);
    }
}