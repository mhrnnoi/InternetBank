using InternetBank.Domain.Accounts;

namespace InternetBank.Domain.Repositories;

public interface IAccountRepository
{
    Account CreateAccount(int accountype, double amount, string userId);
    Task<Account?> GetById(int AccountId, string userId);
    Task<Account?> GetByCardNumber(string cardNumber);
    Task<List<Account>> GetAllAccounts(string userId);
}