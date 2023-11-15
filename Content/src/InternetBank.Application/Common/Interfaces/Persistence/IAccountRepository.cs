using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Accounts.ValueObjects;

namespace InternetBank.Domain.Repositories;

public interface IAccountRepository
{
    void AddAccount(Account account);
    // Task<Account?> GetById(AccountId AccountId);
    // Task<Account?> GetByCardNumber(CardNumber cardNumber);
    Task<List<Account>> GetAllAccounts();
}