using System.Transactions;

namespace InternetBank.Domain.Repositories;

public interface ITransactionRepository
{
    void Add(Domain.Transactions.Transaction transactions);
    Task<Domain.Transactions.Transaction?> GetByOTP(string otp, double amount);
    Task<List<Domain.Transactions.Transaction>> GetByDateAndSuccess(DateOnly? from, DateOnly? to, bool? isSuccess);

    // void Add(List<Transaction> transactions);
    string SendOTP();
}