using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using InternetBank.Application.Common.Interfaces;
using InternetBank.Application.Common.Services;
using InternetBank.Application.Features.Authentication.Commands.Common;
using InternetBank.Domain.Interfaces.IdentityService;
using InternetBank.Domain.Interfaces.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternetBank.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterationResult>
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

    public async Task<RegisterationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        //identity service create user 
        var userId = await _identityservice.CreateUserAsync(request.FirstName,
                                                            request.LastName,
                                                            request.NationalCode,
                                                            request.BirthDate,
                                                            request.Email,
                                                            request.PhoneNumber,
                                                            request.Username,
                                                            request.Password);
        //dbset savechanges

        await _unitOfWork.SaveChangesAsync();
        var res = AuthResultService.CreateRegisterResult(userId, _jwtGenerator.GenerateToken());
var sss =  new IdentityResult();

        throw new ValidationException();
        return res;
        


        //generate register result



    }
}
