using InternetBank.Application.Authentication.Queries.Common;

namespace InternetBank.Application.Interfaces;

public interface IIdentityService
{
    Task<List<UserDTO>> GetAllAsync();
    Task<UserDTO> GetByIdAsync(string id);
    Task<string> CreateUserAsync(string firstName,
                                 string lastName,
                                 string nationalCode,
                                 DateOnly birthDate,
                                 string Email,
                                 string PhoneNumber,
                                 string Username,
                                 string Password);
    
    Task<UserDTO> LoginUserAsync(string Email,
                                string Password);
    // void Delete(ApplicationUser user);
    // void Update(ApplicationUser user);
}