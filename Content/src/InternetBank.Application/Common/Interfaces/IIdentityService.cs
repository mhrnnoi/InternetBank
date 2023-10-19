using InternetBank.Application.Authentication.Queries.Common;

namespace InternetBank.Application.Interfaces;

public interface IIdentityService
{
    // Task<List<ApplicationUser>> GetAllAsync();
    Task<UserDTO?> GetByIdAsync(string id);
    Task<string> CreateUserAsync(string firstName,
                      string lastName,
                      string nationalCode,
                      DateTime birthDate,
                      string Email,
                      string PhoneNumber,
                      string Username,
                      string Password);
    Task<string> LoginUserAsync(string Email,
                                string Password);
    // void Delete(ApplicationUser user);
    // void Update(ApplicationUser user);
}