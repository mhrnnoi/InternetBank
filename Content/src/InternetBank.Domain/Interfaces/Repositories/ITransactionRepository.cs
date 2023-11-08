using InternetBank.Domain.Transactions.Entities;

namespace InternetBank.Domain.Repositories;

public interface ITransactionRepository
{
    void Add(Transaction transactions);
    Task<Transaction?> GetByOTP(string otp,
                                             double amount,
                                            string userId);
    Task<List<Transaction>> GetByDateAndSuccess(DateOnly? from,
                                                             DateOnly? to,
                                                             bool? isSuccess);

     string SendOTP(string receptor,
                   double amount);
}