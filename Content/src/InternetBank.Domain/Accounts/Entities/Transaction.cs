using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.ValueObjects;

namespace InternetBank.Domain.Transactions.Entities;

public sealed class Transaction : Entity<TransactionId>
{
    public bool IsSuccess { get; private set; }
    public double Amount { get; init; }
    public DateTime CreatedDateTime { get; init; }
    public string Description { get; private set; } = string.Empty;
    public CardNumberVO DestinationCardNumber { get; init; }
    public string Otp { get; private set; } = string.Empty;
    public DateTime OtpExpireDate { get; private set; }

    private Transaction(TransactionId transactionId,
                        double amount,
                        CardNumberVO destinationCardNumber) : base(transactionId)
    {
        Amount = amount;
        CreatedDateTime = DateTime.UtcNow;
        IsSuccess = false;
        DestinationCardNumber = destinationCardNumber;
        Description = "در نتظار پرداخت";
    }
    public static ErrorOr<Transaction> CreateTransaction(double amount,
                                                         CardNumberVO destinationCardNumber)
    {
        if (amount < 1000 || amount > 5000000)
            return Errors.Transaction.IncorrectAmountRange;

        return new Transaction(TransactionId.GenerateId(),
                               amount,
                               destinationCardNumber);

    }
    public ErrorOr<string> TransferMoney(double amount,
                                 Cvv2VO cvv2VO,
                                 Cvv2VO accountCvv2VO,
                                 ExpiryDateVO expiryDateVO,
                                 ExpiryDateVO accountExpiryDateVO,
                                PasswordVO password,
                                 string otpOrPassword,
                                 bool isBlockedSource,
                                 bool isBlockedDest)
    {

        if (cvv2VO != accountCvv2VO)
            return Errors.Transaction.IncorrectCVV2;

        if (expiryDateVO != accountExpiryDateVO)
            return Errors.Transaction.IncorrectExpiryDate;

        if ((otpOrPassword != password.Password || amount > 1000000) && (DateTime.UtcNow > OtpExpireDate || otpOrPassword != Otp))
        {
            Description = "عملیات ناموفق - رمز نادرست";
            return Description;
        }
        if (isBlockedSource)
        {
            Description = "عملیات ناموفق - اکانت مبدا مسدود هست";
            return Description;
        }

        if (isBlockedDest)
        {
            Description = "عملیات ناموفق - اکانت مقصد مسدود هست";
            return Description;
        }

        if (Amount < amount)
        {
            Description = "عملیات ناموفق - عدم موجودی";
            return Description;
        }
        IsSuccess = true;
        Description = "عملیات موفق";
        return Description;
    }

    private void SetOtp(string otp)
    {
        Otp = otp;
        OtpExpireDate = DateTime.UtcNow.AddMinutes(2);
    }




}