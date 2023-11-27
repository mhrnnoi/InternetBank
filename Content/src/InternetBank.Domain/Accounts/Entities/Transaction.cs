using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Enums;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Transactions.Enums;

namespace InternetBank.Domain.Transactions.Entities;

public sealed class Transaction : Entity<TransactionId>
{
#pragma warning disable CS8618
    private Transaction()
    {

    }
#pragma warning restore CS8618
    public bool IsSuccess { get; private set; }
    public double Amount { get; init; }
    public DateTime CreatedDateTime { get; init; }
    public Description Description { get; private set; }
    public CardNumber DestinationCardNumber { get; init; }
    public Status Status { get; set; }
    public Otp Otp { get; private set; }
    public OtpExpireDate OtpExpireDate { get; private set; }


    private Transaction(TransactionId transactionId,
                        double amount,
                        CardNumber destinationCardNumber,
                        Otp otp) : base(transactionId)
    {
        Amount = amount;
        Status = Status.Pending;
        CreatedDateTime = DateTime.UtcNow;
        IsSuccess = false;
        DestinationCardNumber = destinationCardNumber;
        Description = Description.GenerateDescription(DescriptionTypes.PendingToPaid);
        Otp = otp;
        OtpExpireDate = OtpExpireDate.GenerateOTPExpiryDate();

    }
    public static ErrorOr<Transaction> CreateTransaction(double amount,
                                                         CardNumber destinationCardNumber,
                                                         Otp otp)
    {
        if (amount < 1000 || amount > 5000000)
            return Errors.Transaction.IncorrectAmountRange;
        return new Transaction(TransactionId.GenerateId(),
                               amount,
                               destinationCardNumber,
                                otp);

    }

    public Description ChangeDescription(DescriptionTypes descriptionTypes)
    {
        if (descriptionTypes is DescriptionTypes.Success)
        {
            IsSuccess = true;
            Status = Status.Success;
            Description = Description.GenerateDescription(descriptionTypes);
            return Description;
        }

        Status = Status.Failed;
        Description = Description.GenerateDescription(descriptionTypes);

        return Description;
    }
    public ErrorOr<Transaction> SendOtp()
    {
        if (OtpExpireDate.Value >= DateTime.UtcNow)
            return Errors.Transaction.OtpLimit;

        Otp = Otp.GenerateOTP();
        OtpExpireDate =  OtpExpireDate.GenerateOTPExpiryDate();
        return this;
    }




}