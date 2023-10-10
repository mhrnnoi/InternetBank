namespace InternetBank.Domain.Interfaces.IdentityService;

public interface IIdentityService
{
    Task<List<ApplicationUser>> GetAllAsync();
    Task<ApplicationUser?> GetByIdAsync(string id);
    bool CreateUserAsync(string firstName,
                         string lastName,
                         string nationalCode,
                         DateTime birthDate,
                         string Email,
                         string PhoneNumber,
                         string Username,
                         string Password);
    void Delete(ApplicationUser user);
    void Update(ApplicationUser user);
}