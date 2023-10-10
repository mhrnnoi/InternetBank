using InternetBank.Domain.Interfaces.IdentityService;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace InternetBank.Infrastructure.Identity;
public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly Mapper _mapper;
    public IdentityService(UserManager<ApplicationUser> userManager, Mapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<bool> CreateUserAsync(string firstName, string lastName, string nationalCode, DateTime birthDate, string Email, string PhoneNumber, string Username, string Password)
    {
        var user =  ApplicationUser.CreateUser(firstName, lastName, nationalCode, birthDate);
        user.UserName = Username;
        user.Email = Email;
        user.PhoneNumber = PhoneNumber;


        
        var res =  await _userManager.CreateAsync(user, Password);
        return res.Succeeded;
    }

    public void Delete(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Task<List<ApplicationUser>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public void Update(ApplicationUser user)
    {
        throw new NotImplementedException();
    }
}