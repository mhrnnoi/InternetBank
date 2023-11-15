using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Transactions.Enums;

namespace InternetBank.Domain.Transactions.Entities;

public sealed class Transaction
{
    public Status Status { get; private set; }
    public double Amount { get; init; }
    public DateTime CreatedDateTime { get; init; }
    public string Description { get; private set; } = string.Empty;
    // public CardNumber DestinationCardNumber { get; init; }
    // public Otp? Otp { get; private set; }
    public DateTime? OtpExpireDate { get; private set; }

    private Transaction(double amount) 
    {
        Status = Status.Pending;
        Amount = amount;
        CreatedDateTime = DateTime.UtcNow;
        // DestinationCardNumber = destinationCardNumber;
        Description = "در نتظار پرداخت";
        
    }
    public static ErrorOr<Transaction> CreateTransaction(double amou)
    {
        // if (amount < 1000 || amount > 5000000)
            return Errors.Transaction.IncorrectAmountRange;

        // return new Transaction(TransactionId.GenerateId(),
        //                        amount,
        //                        destinationCardNumber);

    }


    public void SetOtp(Otp otp)
    {
        // Otp = otp;
        OtpExpireDate = DateTime.UtcNow.AddMinutes(2);
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }
    public void ChangeStatus(Status status)
    {
        Status = status;
    }




}