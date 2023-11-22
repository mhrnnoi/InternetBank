using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Accounts.ValueObjects;
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

    public async Task<Account?> GetByCardNumber(CardNumber cardNumber)
    {
        return await _context.FirstOrDefaultAsync(x => x.CardNumber == cardNumber);
    }



    public async Task<Account?> GetById(string accountId)
    {
        var accId = AccountId.Parse(accountId);
        if (accId is null)
            return null;
        return await _context.FirstOrDefaultAsync(x => x.Id == accId);
    }

    public async Task<List<Account>> GetUserAllAccounts(string UserId)
    {
        return await _context.Where(x => x.UserId == UserId).ToListAsync();
    }
}
