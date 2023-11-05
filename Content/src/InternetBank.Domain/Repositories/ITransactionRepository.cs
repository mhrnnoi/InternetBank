using System.Transactions;

namespace InternetBank.Domain.Repositories;

public interface ITransactionRepository
{
    void Add(Domain.Transactions.Transaction transactions);
    Task<Domain.Transactions.Transaction?> GetByOTP(string otp, double amount);
    // void Add(Transaction transactions);
    // void Add(Transaction transactions);
    string SendOTP();
}