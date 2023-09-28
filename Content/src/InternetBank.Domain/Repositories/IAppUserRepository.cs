using InternetBank.Domain.Users;

namespace InternetBank.Domain.Repositories;

public interface IAppUserRepository
{
    Task<List<ApplicationUser>> GetAllAsync();
    Task<ApplicationUser?> GetByIdAsync(string id);
    void Create(ApplicationUser user);
    void Delete(ApplicationUser user);
    void Update(ApplicationUser user);
}