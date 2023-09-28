using InternetBank.Domain.Repositories;
using InternetBank.Domain.Users;
using InternetBank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Infrastructure.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly DbSet<ApplicationUser> _dbset;
    public AppUserRepository(ApplicationDbContext dbContext)
    {
        _dbset = dbContext.AppUsers;
    }
    public void Create(ApplicationUser user)
    {
        _dbset.Add(user);
        
    }

    public void Delete(ApplicationUser user)
    {
        _dbset.Remove(user);
    }

    public async Task<List<ApplicationUser>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public async Task<ApplicationUser?> GetByIdAsync(string id)
    {
        return await _dbset.FirstOrDefaultAsync(x => x.Id.ToString() == id);
    }

    public void Update(ApplicationUser user)
    {
        _dbset.Update(user);
    }
}
