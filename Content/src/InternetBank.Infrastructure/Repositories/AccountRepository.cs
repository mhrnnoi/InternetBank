using System.Security.Authentication;
using InternetBank.Domain.Accounts;
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

    public Account CreateAccount(int accountype, double amount, string userId)
    {
        var acc = Account.OpenAccount((AccountTypes)accountype, amount, userId);
        _context.Add(acc);
        return acc;

    }

    public async Task<List<Account>> GetAllAccounts(string userId)
    {
        return await _context.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<Account?> GetByCardNumber(string cardNumber)
    {
        return await _context.FirstOrDefaultAsync(x => x.CardNumber == cardNumber);
    }

    public async Task<Account?> GetById(int AccountId, string userId)
    {
        return await _context.FirstOrDefaultAsync(x => x.Id == AccountId && x.UserId == userId);
    }


}
