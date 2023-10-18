using InternetBank.Domain.Accounts;

namespace InternetBank.Domain.Repositories;

public interface IAccountRepository
{
    void AddAccount(Account account);
}