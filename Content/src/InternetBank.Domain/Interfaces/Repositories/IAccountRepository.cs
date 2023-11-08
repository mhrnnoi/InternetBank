
using InternetBank.Domain.Accounts.Entities;

namespace InternetBank.Domain.Repositories;

public interface IAccountRepository
{
    void AddAccount(Account account);
    Task<Account?> GetById(string AccountId,
                           string userId);
    Task<Account?> GetByCardNumber(string cardNumber);
    Task<List<Account>> GetAllAccounts(string userId);
}