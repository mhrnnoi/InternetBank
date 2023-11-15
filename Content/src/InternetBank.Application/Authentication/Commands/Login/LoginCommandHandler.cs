using InternetBank.Application.Common.Interfaces;
using InternetBank.Application.Authentication.Commands.Common;
using InternetBank.Application.Interfaces;
using MediatR;
using ErrorOr;

namespace InternetBank.Application.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<LoginActionResult>>
{
    private readonly IIdentityService _identityservice;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginCommandHandler(IIdentityService identityservice, IJwtGenerator jwtGenerator)
    {
        _identityservice = identityservice;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<ErrorOr<LoginActionResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userOrError = await _identityservice.LoginUserAsync(request.Email,
                                                         request.Password);
        if (userOrError.IsError)
            return userOrError.Errors;

        var output = AuthResultService.CreateLoginResult(userOrError.Value.Id, _jwtGenerator.GenerateToken(userOrError.Value));
        return output;




    }
}
