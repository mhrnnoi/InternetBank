using InternetBank.Application.Common.Interfaces;
using InternetBank.Application.Authentication.Commands.Common;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Interfaces.UOF;
using MediatR;

namespace InternetBank.Application.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginActionResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityservice;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IIdentityService identityservice, IJwtGenerator jwtGenerator)
    {
        _unitOfWork = unitOfWork;
        _identityservice = identityservice;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<LoginActionResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityservice.LoginUserAsync(request.Email, request.Password);
        await _unitOfWork.SaveChangesAsync();
        var output = AuthResultService.CreateLoginResult(user.Id, _jwtGenerator.GenerateToken(user));
        return output;




    }
}
