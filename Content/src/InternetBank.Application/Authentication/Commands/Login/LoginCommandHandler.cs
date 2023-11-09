using InternetBank.Application.Common.Interfaces;
using InternetBank.Application.Authentication.Commands.Common;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Interfaces.UOF;
using MediatR;
using ErrorOr;
using InternetBank.Domain.Common.Errors;

namespace InternetBank.Application.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<LoginActionResult>>
{
    private readonly IIdentityService _identityservice;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IIdentityService identityservice, IJwtGenerator jwtGenerator)
    {
        _identityservice = identityservice;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<ErrorOr<LoginActionResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userOrError = await _identityservice.LoginUserAsync(request.Email,
                                                         request.Password);
                                                         if(userOrError.IsError)
                                                         {
                                                            return userOrError.Errors;
                                                         }
        var output = AuthResultService.CreateLoginResult(userOrError.Value.Id, _jwtGenerator.GenerateToken(userOrError.Value));
        return output;




    }
}
