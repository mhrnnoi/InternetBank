using ErrorOr;
using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Exceptions.User;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ErrorOr<string>> CreateUserAsync(string firstName,
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
            return Errors.User.AlreadyExistNationalCode;

        var isPhoneNumberUnique = await IsPhoneNumberUnique(PhoneNumber);
        if (!isPhoneNumberUnique)
            return Errors.User.AlreadyExistPhoneNumber;

        var user = ApplicationUser.CreateUser(firstName,
                                              lastName,
                                              nationalCode,
                                              birthDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
                                              Username,
                                              Email,
                                              PhoneNumber);

        var res = await _userManager.CreateAsync(user.Value, Password);

        if (res.Succeeded)
            return user.Value.Id;

        var failures = GetErrors(res);
        return failures;

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

    private static List<Error> GetErrors(IdentityResult result)
    {
        var failures = new List<Error>();
        foreach (var item in result.Errors)
        {
            failures.Add(Error.Validation(item.Code, item.Description));
        }

        return failures;
    }




    public async Task<UserDTO> GetByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? throw new NotFoundUserById();
        return _mapper.Map<UserDTO>(user);

    }

    public async Task<ErrorOr<UserDTO>> LoginUserAsync(string Email, string Password)
    {

        var user = await _userManager.FindByEmailAsync(Email);
        if (user is not null)
        {
            var result = await _userManager.CheckPasswordAsync(user, Password);
            if (result)
                return _mapper.Map<UserDTO>(user);

            return Errors.User.InvalidCred;

        }
        else
        {
            return Errors.User.InvalidCred;
        }

    }

    public async Task<List<UserDTO>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return _mapper.Map<List<UserDTO>>(users);
    }

}