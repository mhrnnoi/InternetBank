using InternetBank.Application.Authentication.Commands.Login;
using InternetBank.Application.Authentication.Commands.Register;

namespace InternetBank.Application.Authentication.Commands.Common;

public class AuthResultService
{
    public static RegisterActionResult CreateRegisterResult(string id)
    {
        return new RegisterActionResult("Registered Successfully..",
                                        id);
    }
    public static LoginActionResult CreateLoginResult(string id,
                                                      string token)
    {
        return new LoginActionResult("Login succesfull",
                                       id,
                                       token);
    }
}