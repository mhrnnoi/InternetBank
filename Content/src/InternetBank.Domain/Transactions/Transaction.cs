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
    public DateTime SourceCardExpireDate { get; set; }
    public string UserId { get; set; }

    private Transaction(double amount, string destinationCardNumber, string sourceCardNumber, string cVV2, DateTime sourceCardExpireDate, string oTP, string userId)
    {
        Amount = amount;
        CreatedDateTime = DateTime.UtcNow;
        IsSuccess = false;
        DestinationCardNumber = destinationCardNumber;
        SourceCardNumber = sourceCardNumber;
        CVV2 = cVV2;
        SourceCardExpireDate = sourceCardExpireDate;
        OTP = oTP;
        UserId = userId;
    }
    public string TransferMoney(double amount, string description, int oTP)
    {

        Amount = amount;

        // amount == Amount;
   




        return "";



    }
    public static Transaction CreateTransaction(double amount,
                                                string destinationCardNumber,
                                                string sourceCardNumber,
                                                string cVV2,
                                                DateTime sourceCardExpireDate,
                                                string otp,
                                                string userId)
    {
        CheckCardNumberFormat(sourceCardNumber);
        CheckCVV2(cVV2);
        CheckExpireDate(sourceCardExpireDate);
        CheckAmount(amount);
        CheckCardNumberFormat(destinationCardNumber);
        
        return new Transaction(amount,
                               destinationCardNumber,
                               sourceCardNumber,
                               cVV2,
                               sourceCardExpireDate,
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

    private static bool CheckExpireDate(DateTime expiryDate)
    {
        if (expiryDate <= DateTime.UtcNow)
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