using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Accounts.ValueObjects;

namespace InternetBank.Domain.Repositories;

public interface ITransactionRepository
{
    bool SendEmailForCreatingAccount(string destinationEmail,
                                     string accountType,
                                     double amount,
                                     string accountNumber,
                                     string cardNumber);
    bool SendEmailForCreatingTransaction(string destinationEmail,
                                         double Amount,
                                         string DestinationCardNumber,
                                         string SourceCardNumber,
                                         string UserId);
    bool SendEmailForCompleteTransaction(string destinationEmail,
                                         double Amount,
                                         string DestinationCardNumber,
                                         string SourceCardNumber,
                                         string UserId);

    string SendOTP(string receptor,
                   double amount,
                   Otp otp);
}