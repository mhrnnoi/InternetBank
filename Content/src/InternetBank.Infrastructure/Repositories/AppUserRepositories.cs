using InternetBank.Domain.Repositories;
using InternetBank.Domain.Users;

namespace InternetBank.Infrastructure.Repositories;

public class AppUserRepositories : IAppUserRepository
{
    public bool Create(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ApplicationUser>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public bool Update(ApplicationUser user)
    {
        throw new NotImplementedException();
    }
}
