namespace InternetBank.Domain.Repositories;

public interface ITransactionRepository
{
    void Add(Transactions.Transaction transactions);
    Task<Transactions.Transaction?> GetByOTP(string otp,
                                             double amount,
                                            string userId);
    Task<List<Transactions.Transaction>> GetByDateAndSuccess(DateOnly? from,
                                                             DateOnly? to,
                                                             bool? isSuccess);

     string SendOTP(string receptor,
                   double amount);
}