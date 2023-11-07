using InternetBank.Domain.Accounts;

namespace InternetBank.Domain.Repositories;

public interface IAccountRepository
{
    void AddAccount(Account account);
    Task<Account?> GetById(int AccountId,
                           string userId);
    Task<Account?> GetByCardNumber(string cardNumber);
    Task<List<Account>> GetAllAccounts(string userId);
}