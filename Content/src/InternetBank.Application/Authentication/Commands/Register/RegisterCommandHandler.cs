using InternetBank.Application.Authentication.Commands.Common;
using InternetBank.Application.Common.Interfaces;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Interfaces.UOF;
using MediatR;

namespace InternetBank.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterActionResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityservice;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IIdentityService identityservice, IJwtGenerator jwtGenerator)
    {
        _unitOfWork = unitOfWork;
        _identityservice = identityservice;
    }

    public async Task<RegisterActionResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var id = await _identityservice.CreateUserAsync(request.FirstName,
                                                        request.LastName,
                                                        request.NationalCode,
                                                        request.BirthDate,
                                                        request.Email,
                                                        request.PhoneNumber,
                                                        request.Username,
                                                        request.Password);

        await _unitOfWork.SaveChangesAsync();
        return AuthResultService.CreateRegisterResult(id);

    }

    
}
