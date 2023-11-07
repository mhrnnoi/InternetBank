using InternetBank.Domain.Accounts;
using static InternetBank.Domain.Exceptions.DomainExceptions.Transaction;
using static InternetBank.Domain.Exceptions.Transaction;

namespace InternetBank.Domain.Transactions;

public sealed class Transaction
{
    public int Id { get; set; }
    public bool IsSuccess { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string Description { get; set; } = string.Empty;
    public string SourceCardNumber { get; set; }
    public string DestinationCardNumber { get; set; } = string.Empty;
    public string OTP { get; private init; }
    public DateTime OTPExpireDate { get; set; }
    public string CVV2 { get; set; }
    public string SourceCardExpireYear { get; set; }
    public string SourceCardExpireMonth { get; set; }
    public string UserId { get; set; }

    private Transaction(double amount,
                        string destinationCardNumber,
                        string sourceCardNumber,
                        string cVV2,
                        string sourceCardExpireYear,
                        string sourceCardExpireMonth,
                        string oTP,
                        string userId)
    {
        Amount = amount;
        CreatedDateTime = DateTime.UtcNow;
        IsSuccess = false;
        DestinationCardNumber = destinationCardNumber;
        SourceCardNumber = sourceCardNumber;
        CVV2 = cVV2;
        SourceCardExpireYear = sourceCardExpireYear;
        SourceCardExpireMonth = sourceCardExpireMonth;
        OTP = oTP;
        UserId = userId;
        Description = "عملیات ناموفق به دلیل انجام ندادن کاری یا گذشتن زمان مجاز";
    }
    public string TransferMoney(Account account, Account account1, string userId)
    {
        if (UserId != userId)
        {
            throw new NotYourTransaction();

        }
        if (IsSuccess)
        {
            throw new AlreadyCompletedTransaction();
        }

        if (DateTime.UtcNow > OTPExpireDate)
        {
            Description = "عملیات ناموفق - رمز نادرست";
        }
        if (account.IsBlocked)
        {
            Description = "عملیات ناموفق - اکانت مبدا مسدود هست";
        }
        if (account1.IsBlocked)
        {
            Description = "عملیات ناموفق - اکانت مقصد مسدود هست";
        }
        if (account.Amount >= Amount)
        {
            Description = "عملیات ناموفق - عدم موجودی";
        }
        account.Withdrawl(Amount);
        account1.Deposit(Amount);
        IsSuccess = true;
        Description = "عملیات موفق";
        return Description;



    }
    public static Transaction CreateTransaction(double amount,
                                                string destinationCardNumber,
                                                string sourceCardNumber,
                                                string cVV2,
                                                string sourceCardExpireYear,
                                                string sourceCardExpireMonth,
                                                string otp,
                                                string userId)
    {
        CheckCardNumberFormat(sourceCardNumber);
        CheckCVV2(cVV2);
        CheckExpireYear(sourceCardExpireYear);
        CheckExpireMonth(sourceCardExpireMonth);
        CheckAmount(amount);
        CheckCardNumberFormat(destinationCardNumber);

        return new Transaction(amount,
                               destinationCardNumber,
                               sourceCardNumber,
                               cVV2,
                               sourceCardExpireYear,
                               sourceCardExpireMonth,
                               otp,
                               userId);
    }


    private static bool CheckAmount(double amount)
    {
        if (amount < 1000 || amount > 5000000)
        {
            return false;
        }
        return true;
    }

    private static bool CheckExpireYear(string expiryYear)
    {
        if (int.Parse(expiryYear) <= DateTime.UtcNow.Year)
        {
            return true;
        }
        return false;
    }
    private static bool CheckExpireMonth(string expiryMonth)
    {
        if (int.Parse(expiryMonth) <= DateTime.UtcNow.Month)
        {
            return true;
        }
        return false;
    }

    private static bool CheckCVV2(string cVV2)
    {
        return cVV2.All(x => char.IsDigit(x))
                && cVV2.Length == 4;
    }

    private static bool CheckCardNumberFormat(string cardNumber)
    {

        return cardNumber.All(x => char.IsDigit(x))
                 && cardNumber.Length == 16;
    }
    public string Report()
    {
        return "";
    }
}