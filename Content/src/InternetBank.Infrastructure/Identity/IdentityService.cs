using FluentValidation.Results;
using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Exceptions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Infrastructure.Identity;
public class IdentityService : IIdentityService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }
    public async Task<string> CreateUserAsync(string firstName,
                                              string lastName,
                                              string nationalCode,
                                              DateTime birthDate,
                                              string Email,
                                              string PhoneNumber,
                                              string Username,
                                              string Password)
    {
        var user = ApplicationUser.CreateUser(firstName,
                                              lastName,
                                              nationalCode,
                                              birthDate,
                                              Username,
                                              Email,
                                              PhoneNumber);

        var res = await _userManager.CreateAsync(user, Password);
        if (res.Succeeded)
        {
            return user.Id;
        }
        var failures = GetErrors(res);
        throw new FluentValidation.ValidationException(failures);






    }
    private static List<ValidationFailure> GetErrors(IdentityResult result)
    {
        var failures = new List<ValidationFailure>();
        foreach (var item in result.Errors)
        {
            failures.Add(new ValidationFailure(item.Code, item.Description));
        }

        return failures;
    }




    public async Task<UserDTO> GetByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            throw new DomainExceptions.User.NotFoundUserById();
        }
        return new UserDTO(user.FirstName, user.LastName);
    }

    public async Task<string> LoginUserAsync(string Email, string Password)
    {

        var user = await _userManager.FindByEmailAsync(Email);
        if (user is not null)
        {
            var result = await _userManager.CheckPasswordAsync(user, Password);
            if (result)
            {
                return user.Id;
            }
        }

        throw new DomainExceptions.User.InvalidCred();
    }

    public async Task<List<UserDTO>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return _mapper.Map<List<UserDTO>>(users);

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