using FluentValidation.Results;
using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static InternetBank.Domain.Exceptions.DomainExceptions.User;

namespace InternetBank.Infrastructure.Identity;
public class IdentityService : IIdentityService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    public IdentityService(UserManager<ApplicationUser> userManager,
                           IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<string> CreateUserAsync(string firstName,
                                              string lastName,
                                              string nationalCode,
                                              DateOnly birthDate,
                                              string Email,
                                              string PhoneNumber,
                                              string Username,
                                              string Password)
    {

        var isNationalCodeUnique = await IsNationalCodeUnique(nationalCode);
        if (!isNationalCodeUnique)
            throw new AlreadyExistNationalCode();

        var isPhoneNumberUnique = await IsPhoneNumberUnique(PhoneNumber);
        if (!isPhoneNumberUnique)
            throw new AlreadyExistPhoneNumber();

        var user = ApplicationUser.CreateUser(firstName,
                                              lastName,
                                              nationalCode,
                                              birthDate,
                                              Username,
                                              Email,
                                              PhoneNumber);

        var res = await _userManager.CreateAsync(user, Password);

        if (res.Succeeded)
            return user.Id;

        var failures = GetErrors(res);
        throw new FluentValidation.ValidationException(failures);

    }

    private async Task<bool> IsPhoneNumberUnique(string phoneNumber)
    {
        if (await _userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber))
            return false;
        return true;

    }

    private async Task<bool> IsNationalCodeUnique(string nationalCode)
    {
        if (await _userManager.Users.AnyAsync(x => x.NationalCode == nationalCode))
            return false;
        return true;
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
        var user = await _userManager.FindByIdAsync(id) ?? throw new NotFoundUserById();
        return _mapper.Map<UserDTO>(user);

    }

    public async Task<UserDTO> LoginUserAsync(string Email, string Password)
    {

        var user = await _userManager.FindByEmailAsync(Email);
        if (user is not null)
        {
            var result = await _userManager.CheckPasswordAsync(user, Password);
            if (result)
                return _mapper.Map<UserDTO>(user);

            throw new InvalidCred();

        }

        throw new InvalidCred();
    }

    public async Task<List<UserDTO>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return _mapper.Map<List<UserDTO>>(users);
    }

}