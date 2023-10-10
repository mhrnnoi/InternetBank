using InternetBank.Domain.Accounts;
using InternetBank.Domain.Exceptions;

namespace InternetBank.Domain.Transactions;

public sealed  class Transaction
{
    public int Id { get; set; }
    public bool IsSuccess { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public int AccountId { get; set; }
    public string Description { get; set; }
    public string DestinationCardNumber { get; set; } = string.Empty;
    public int OTP { get; set; }
    public DateTime OTPExpireDate { get; set; }
    public Status Status { get; set; }

    private Transaction(double amount, int accountId, string destinationCardNumber, int oTP, string description)
    {
        Amount = amount;
        CreatedDateTime = DateTime.UtcNow;
        IsSuccess = false;
        AccountId = accountId;
        DestinationCardNumber = destinationCardNumber;
        OTP = oTP;
        Status = Status.Pending;
        Description = description;
    }
    public string TransferMoney(double amount, string description, int oTP)
    {

        Amount = amount;
        
        // amount == Amount;
        OTP = oTP;

        


        return "";



    }
    public static string SendOTP(string sourceCardNumber,
                                 string cVV2,
                                 DateTime expiryDate,
                                 double amount,
                                 string destinationCardNumber,
                                 int accountId)
    {
        CheckCardNumberFormat(sourceCardNumber);
        CheckCVV2(cVV2);
        CheckExpireDate(expiryDate);
        CheckAmount(amount);
        CheckCardNumberFormat(destinationCardNumber);

        var transaction = new Transaction(amount, accountId, destinationCardNumber, 6464, "desc");
        transaction.OTPExpireDate = DateTime.Now.AddMinutes(1);

        return "";



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
            return false;
        }
        return true;
    }

    private static bool CheckCVV2(string cVV2)
    {
        if (cVV2.Length != 4)
        {
            return false;
        }
        if (cVV2.FirstOrDefault(x => char.IsDigit(x) != true) != 0)
        {
            return false;
        }
        return true;
    }

    private static bool CheckCardNumberFormat(string cardNumber)
    {
        var cardArr = cardNumber.Split();
        if (cardArr.Length != 4)
        {
            return false;
        }
        foreach (var item in cardArr)
        {
            if (item.Length != 4)
            {
                return false;
            }
            if (item.FirstOrDefault(x => char.IsDigit(x) != true) != 0)
            {
                return false;
            }
        }
        return true;
    }
    public string Report()
    {
        return "";
    }
}