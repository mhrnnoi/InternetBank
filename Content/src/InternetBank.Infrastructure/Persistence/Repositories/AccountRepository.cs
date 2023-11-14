using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Repositories;
using InternetBank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly DbSet<Account> _context;
    public AccountRepository(ApplicationDbContext context)
    {
        _context = context.Accounts;
    }

    public void AddAccount(Account account)
    {
        _context.Add(account);
    }

    public async Task<List<Account>> GetAllAccounts()
    {
        return await _context.ToListAsync();
    }

    public async Task<Account?> GetByCardNumber(string cardNumber)
    {
        return await _context.FirstOrDefaultAsync(x => x.CardNumber == cardNumber);
    }

    public async Task<Account?> GetById(string AccountId)
    {
        return await _context.FirstOrDefaultAsync(x => x.Id == AccountId);
    }


}
