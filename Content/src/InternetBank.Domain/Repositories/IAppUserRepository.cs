using InternetBank.Domain.Users;

namespace InternetBank.Domain.Repositories;

public interface IAppUserRepository
{
    Task<List<ApplicationUser>> GetAll();
    Task<ApplicationUser> GetById(string id);
    bool Create(ApplicationUser user);
    bool Delete(string id);
    bool Update(ApplicationUser user);
}