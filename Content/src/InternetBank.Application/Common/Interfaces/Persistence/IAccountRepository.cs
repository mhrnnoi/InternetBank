using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Accounts.ValueObjects;

namespace InternetBank.Domain.Repositories;

public interface IAccountRepository
{
    void AddAccount(Account account);
    Task<Account?> GetById(string AccountId);
    Task<Account?> GetByCardNumber(CardNumber cardNumber);
    Task<List<Account>> GetAllAccounts();
    Task<List<Account>> GetUserAllAccounts(string UserId);
}