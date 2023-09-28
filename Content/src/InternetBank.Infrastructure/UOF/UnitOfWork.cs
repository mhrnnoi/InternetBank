using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Infrastructure.Data;

namespace InternetBank.Infrastructure.UOF;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
