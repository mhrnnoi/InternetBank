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

    public RegisterCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppUserRepository appUserRepository, UserManager<IdentityUser> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _appUserRepository = appUserRepository;
        _userManager = userManager;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var identityUser = new IdentityUser()
        {
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };




        var appUser = ApplicationUser.CreateUser(request.FirstName, request.LastName, request.NationalCode, request.BirthDate, identityUser.Id);
        if (appUser is not null)
        {
            var userRegisterResult = await _userManager.CreateAsync(identityUser);
            if (userRegisterResult.Succeeded)
            {
                _appUserRepository.Create(appUser);
                await _unitOfWork.SaveChangesAsync();


                return true;
            }

        }


        return false;

    }
}
