using InternetBank.Domain.Accounts;

namespace InternetBank.Domain.Repositories;

public interface IAccountRepository
{
    Account CreateAccount(int accountype, double amount,  string userId);
    Task ChangePassword(int AccountId, string OldPassword, string NewPassword, string RepeatNewPassword);
}