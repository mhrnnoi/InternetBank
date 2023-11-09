using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Common.Errors;

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
    public ErrorOr<string> TransferMoney(Account sourceAccount,
                                         Account destinationAcc,
                                         string userId)
    {
        if (UserId != userId)
            return Errors.Transaction.NotYourTransaction;

        if (IsSuccess)
            return Errors.Transaction.AlreadyCompletedTransaction;

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
    public static ErrorOr<Transaction> CreateTransaction(Account sourceAccount,
                                                Account destinationAccount,
                                                string expiryYear,
                                                string expiryMonth,
                                                double amount,
                                                string cvv2,
                                                string userId)
    {
        var inputCheckErrors = CheckInputs(sourceAccount,
                                     destinationAccount,
                                     expiryYear,
                                     expiryMonth,
                                     amount,
                                     cvv2);
        if (inputCheckErrors.Any())
            return inputCheckErrors;

        var accountCheckErrors = CheckAccounts(sourceAccount,
                                         destinationAccount,
                                         expiryYear,
                                         expiryMonth,
                                         userId,
                                         cvv2);

        if (accountCheckErrors.Any())
            return accountCheckErrors;
            
        return new Transaction(amount,
                               destinationAccount.CardNumber,
                               userId,
                               sourceAccount.Id);
    }
    private static List<Error> CheckInputs(Account sourceAccount,
                                           Account destinationAccount,
                                           string expiryYear,
                                           string expiryMonth,
                                           double amount,
                                           string cvv2)

    {
        var errors = new List<Error>();

        if (!CheckCardNumberFormat(sourceAccount.CardNumber))
            errors.Add(Errors.Transaction.SourceIncorrectCardNumber);

        if (!CheckCardNumberFormat(destinationAccount.CardNumber))
            errors.Add(Errors.Transaction.DestinationIncorrectCardNumber);

        if (!CheckCVV2(cvv2))
            errors.Add(Errors.Transaction.IncorrectCVV2);

        if (!CheckExpiry(expiryYear, expiryMonth))
            errors.Add(Errors.Account.ExpiredAccount);

        if (!CheckAmount(amount))
            errors.Add(Errors.Transaction.IncorrectAmountRange);

        return errors;
    }
    private static List<Error> CheckAccounts(Account sourceAccount,
                                             Account destinationAccount,
                                             string expiryYear,
                                             string expiryMonth,
                                             string userId,
                                             string cvv2)
    {
        var errors = new List<Error>();

        if (sourceAccount.UserId != userId)
        {
            errors.Add(Errors.Account.AccountIsNotYours);
            return errors;
        }

        if (sourceAccount.IsBlocked)
            errors.Add(Errors.Transaction.SourceAccountIsBlocked);

        if (destinationAccount.IsBlocked)
            errors.Add(Errors.Transaction.DestinationAccountIsBlocked);

        if (sourceAccount.ExpiryYear != expiryYear || sourceAccount.ExpiryMonth != expiryMonth)
            errors.Add(Errors.Transaction.IncorrectExpiryDate);

        if (sourceAccount.Cvv2 != cvv2)
            errors.Add(Errors.Transaction.IncorrectCVV2);
        return errors;
    }


    private static bool CheckAmount(double amount)
    {
        if (amount < 1000 || amount > 5000000)
            return false;
        return true;
    }

    private static bool CheckExpiry(string expiryYear, string expiryMonth)
    {
        if (Convert.ToDateTime(expiryYear + "/" + expiryMonth) < DateTime.UtcNow)
            return false;
        return true;

    }

    private static bool CheckCVV2(string cVV2)
    {
        if (!(cVV2.All(x => char.IsDigit(x))
                && cVV2.Length == 4))
            return false;
        return true;
    }

    private static bool CheckCardNumberFormat(string cardNumber)
    {

        if (!(cardNumber.All(char.IsDigit)
                 && cardNumber.Length == 16))
            return false;
        return true;
    }
    public void SetOtp(string otp)
    {
        Otp = otp;
        OtpExpireDate = DateTime.UtcNow.AddMinutes(2);
    }

}