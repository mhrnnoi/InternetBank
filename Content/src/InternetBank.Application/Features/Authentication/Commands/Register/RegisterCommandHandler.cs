using InternetBank.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InternetBank.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var identityUser = new IdentityUser()
        {
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };
        
        var userRegisterResult = await _userManager.CreateAsync(identityUser);
        
        if (userRegisterResult.Succeeded)
        {
            var appUser = ApplicationUser.CreateUser(request.FirstName, request.LastName, request.NationalCode, request.BirthDate, identityUser.Id);
            return true;
        }
        return false;
        
    }
}
