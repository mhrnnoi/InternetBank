using System.Security.Principal;
using InternetBank.Domain.Interfaces.IdentityService;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using InternetBank.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternetBank.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IIdentityService _identityservice;

    public RegisterCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppUserRepository appUserRepository, UserManager<IdentityUser> userManager, IIdentityService identityservice)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _appUserRepository = appUserRepository;
        _userManager = userManager;
        _identityservice = identityservice;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        //identity service create user 
        var userRegisterResult = _identityservice.CreateUserAsync(request.FirstName,
                                         request.LastName,
                                         request.NationalCode,
                                         request.BirthDate,
                                         request.Email,
                                         request.PhoneNumber,
                                         request.Username,
                                         request.Password);
        //dbset savechanges





        {

            if (userRegisterResult)
            {
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

        }


        return false;

    }
}
