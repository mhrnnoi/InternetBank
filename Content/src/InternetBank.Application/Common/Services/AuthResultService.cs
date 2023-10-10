using InternetBank.Application.Features.Authentication.Commands.Common;

namespace InternetBank.Application.Common.Services;

public class AuthResultService
{
    public static RegisterationResult CreateRegisterResult(string id,
                                                           string token)
    {
        return new RegisterationResult("registeration succesfull",
                                       id,
                                       $"User/id={id}",
                                       token);
    }
}