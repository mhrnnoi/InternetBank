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

    public async Task ChangePassword(int AccountId, string OldPassword, string NewPassword, string RepeatNewPassword)
    {
        var acc = await _context.FirstOrDefaultAsync(x => x.Id == AccountId);
        if (acc is null)
        {
            throw new Exception();
        }
        else
        {
            if (acc.Password != OldPassword)
            {
                throw new Exception();
            }
            else
            {

            }
        }
    }

    public Account CreateAccount(int accountype, double amount, string userId)
    {
        var acc = Account.OpenAccount((AccountTypes)accountype, amount, userId);
        _context.Add(acc);
        return acc;

    }
}
