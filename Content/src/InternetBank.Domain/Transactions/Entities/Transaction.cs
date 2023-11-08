using InternetBank.Domain.Abstracts.Entity.Primitives;
using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Exceptions.Account;
using InternetBank.Domain.Exceptions.Transaction;
using InternetBank.Domain.ValueObjects;

namespace InternetBank.Domain.Transactions.Entities;

public sealed class Transaction : Entity
{
    public string AccountId { get; set; }
    public bool IsSuccess { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string Description { get; set; } = string.Empty;
    public string DestinationCardNumber { get; set; } = string.Empty;
    public string Otp { get; private set; } = string.Empty;
    public DateTime OtpExpireDate { get; private set; }
    public string UserId { get; set; }

    private Transaction(double amount,
                        string destinationCardNumber,
                        string userId,
                        string accountId)
    {
        Amount = amount;
        CreatedDateTime = DateTime.UtcNow;
        IsSuccess = false;
        DestinationCardNumber = destinationCardNumber;
        UserId = userId;
        Description = "عملیات ناموفق به دلیل انجام ندادن کاری یا گذشتن زمان مجاز";
        AccountId = accountId;
    }
    public string TransferMoney(Account sourceAccount,
                                Account destinationAcc,
                                string userId)
    {
        if (UserId != userId)
            throw new NotYourTransaction();

        if (IsSuccess)
            throw new AlreadyCompletedTransaction();

        if (DateTime.UtcNow > OtpExpireDate)
            Description = "عملیات ناموفق - رمز نادرست";

        if (sourceAccount.IsBlocked)
            Description = "عملیات ناموفق - اکانت مبدا مسدود هست";

        if (destinationAcc.IsBlocked)
            Description = "عملیات ناموفق - اکانت مقصد مسدود هست";

        if (sourceAccount.Amount < Amount)
            Description = "عملیات ناموفق - عدم موجودی";

        sourceAccount.Withdrawl(Amount);
        destinationAcc.Deposit(Amount);
        IsSuccess = true;
        Description = "عملیات موفق";
        return Description;



    }
    public static Transaction CreateTransaction(Account sourceAccount,
                                                Account destinationAccount,
                                                string expiryYear,
                                                string expiryMonth,
                                                double amount,
                                                string cvv2,
                                                string userId)
    {
        CheckCardNumberFormat(sourceAccount.CardNumber);
        CheckCardNumberFormat(destinationAccount.CardNumber);
        CheckCVV2(cvv2);
        CheckExpiry(expiryYear, expiryMonth);
        CheckAmount(amount);
        CheckAccounts(sourceAccount,
                      destinationAccount,
                      expiryYear,
                      expiryMonth,
                      userId,
                      cvv2);

        return new Transaction(amount,
                               destinationAccount.CardNumber,
                               userId,
                               sourceAccount.Id);
    }
    private static void CheckAccounts(Account sourceAccount,
                                      Account destinationAccount,
                                      string expiryYear,
                                      string expiryMonth,
                                      string userId,
                                      string cvv2)
    {

        if (sourceAccount.UserId != userId)
            throw new AccountIsNotYours();

        if (sourceAccount.IsBlocked)
            throw new AccountIsBlocked("source account is blocked");

        if (destinationAccount.IsBlocked)
            throw new AccountIsBlocked("destination account is blocked");

        if (sourceAccount.ExpiryYear != expiryYear || sourceAccount.ExpiryMonth != expiryMonth)
            throw new IncorrectExpiryDate();

        if (sourceAccount.Cvv2 != cvv2)
            throw new IncorrectCVV2();
    }


    private static void CheckAmount(double amount)
    {
        if (amount < 1000 || amount > 5000000)
            throw new IncorrectAmountRange();
    }

    private static void CheckExpiry(string expiryYear, string expiryMonth)
    {
        if (Convert.ToDateTime(expiryYear + "/" + expiryMonth) < DateTime.UtcNow)
            throw new ExpiredAccount();

    }

    private static void CheckCVV2(string cVV2)
    {
        if (!(cVV2.All(x => char.IsDigit(x))
                && cVV2.Length == 4))
            throw new IncorrectCVV2();
    }

    private static void CheckCardNumberFormat(string cardNumber)
    {

        if (!(cardNumber.All(char.IsDigit)
                 && cardNumber.Length == 16))
            throw new IncorrectCardNumber();
    }
    public void SetOtp(string otp)
    {
        Otp = otp;
        OtpExpireDate = DateTime.UtcNow.AddMinutes(2);
    }

}