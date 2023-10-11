using Microsoft.AspNetCore.Identity;

namespace InternetBank.Application.Interfaces;

public interface IIdentityService
{
    // Task<List<ApplicationUser>> GetAllAsync();
    // Task<ApplicationUser?> GetByIdAsync(string id);
    Task<(IdentityResult result, string id)> CreateUserAsync(string firstName,
                                                              string lastName,
                                                              string nationalCode,
                                                              DateTime birthDate,
                                                              string Email,
                                                              string PhoneNumber,
                                                              string Username,
                                                              string Password);
    // void Delete(ApplicationUser user);
    // void Update(ApplicationUser user);
}