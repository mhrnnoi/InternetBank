using InternetBank.Domain.Transactions.Entities;

namespace InternetBank.Domain.Repositories;

public interface ITransactionRepository
{
    void Add(Transaction transactions);
    Task<Transaction?> GetByOTP(string otp,
                                double amount);
    Task<List<Transaction>> GetByDateAndSuccess(DateOnly from,
                                                DateOnly to,
                                                bool isSuccess);
    Task<List<Transaction>> GetLastFive();

     string SendOTP(string receptor,
                   double amount);
}