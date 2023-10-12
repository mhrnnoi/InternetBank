using FluentValidation.Results;
using InternetBank.Application.Common.Interfaces;
using InternetBank.Application.Common.Services;
using InternetBank.Application.Features.Authentication.Commands.Common;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Interfaces.UOF;
using MediatR;

namespace InternetBank.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterActionResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityservice;
    private readonly IJwtGenerator _jwtGenerator;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IIdentityService identityservice, IJwtGenerator jwtGenerator)
    {
        _unitOfWork = unitOfWork;
        _identityservice = identityservice;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<RegisterActionResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        //identity service create user 
        var (result, id) = await _identityservice.CreateUserAsync(request.FirstName,
                                                            request.LastName,
                                                            request.NationalCode,
                                                            request.BirthDate,
                                                            request.Email,
                                                            request.PhoneNumber,
                                                            request.Username,
                                                            request.Password);


        //dbset savechanges

        if (!result.Succeeded)
        {
            var failures = new List<ValidationFailure>();
            foreach (var item in result.Errors)
            {
                failures.Add(new ValidationFailure(item.Code, item.Description));
            }
            throw new FluentValidation.ValidationException(failures);


        }

        await _unitOfWork.SaveChangesAsync();
        var output = AuthResultService.CreateRegisterResult(id, _jwtGenerator.GenerateToken());
        return output;











        //generate register result



    }
}
