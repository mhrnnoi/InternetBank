using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InternetBank.Infrastructure.Identity;
public class IdentityService : IIdentityService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<(IdentityResult result, string id)> CreateUserAsync(string firstName,
                                                                           string lastName,
                                                                           string nationalCode,
                                                                           DateTime birthDate,
                                                                           string Email,
                                                                           string PhoneNumber,
                                                                           string Username,
                                                                           string Password)
    {
        var user = ApplicationUser.CreateUser(firstName, lastName, nationalCode, birthDate);
        user.UserName = Username;
        user.Email = Email;
        user.PhoneNumber = PhoneNumber;



        var res = await _userManager.CreateAsync(user, Password);
        if (res.Succeeded)
        {
            return (res, user.Id);
        }
        return (res, string.Empty);

    }

    public async Task<UserDTO?> GetByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return null;
        }
        return new UserDTO(user.FirstName, user.LastName);
    }

    public async Task<(bool res, string id, string username)> LoginUserAsync(string Email, string Password)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user is not null)
        {
            var result = await _userManager.CheckPasswordAsync(user, Password);
            if (result)
            {
                return (true, user.Id, user.UserName ??= "");
            }
        }

        return (false, string.Empty, string.Empty);
    }

    // public void Delete(ApplicationUser user)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<List<ApplicationUser>> GetAllAsync()
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<ApplicationUser?> GetByIdAsync(string id)
    // {
    //     throw new NotImplementedException();
    // }

    // public void Update(ApplicationUser user)
    // {
    //     throw new NotImplementedException();
    // }
}