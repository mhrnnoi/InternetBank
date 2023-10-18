using InternetBank.Domain.Exceptions;

namespace InternetBank.Domain.Accounts;

public sealed class Account
{
    public int Id { get; set; }
    public int Type { get; private set; }
    public double Amount { get; private set; }
    public string Number { get; private set; }
    public string CardNumber { get; private set; }
    public string UserId { get; init; }
    public string CVV2 { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public string Password { get; private set; }
    public bool IsBlocked { get; private set; }
    private Account(int type, double amount, string userId)
    {

        Type = type;
        Amount = amount;
        UserId = userId;
        IsBlocked = false;
        Number = GenerateAccountNumber();
        CardNumber = GenerateCartNumber();
        CVV2 = GenerateCVV2();
        Password = GeneratePassword();
        ExpiryDate = SetExpiry();
    }
    public string Balance()
    {
        
        return "" + Amount + "\n" + Id + "\n" + Number;
    }
    public string Report()
    {
        return "" + Password;
    }
    public void BlockAccount()
    {
        this.IsBlocked = true;
    }
    public void UnBlockAccount()
    {
        this.IsBlocked = false;
    }
    public string GeneratePassword()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 6; i++)
        {
            str += rnd.Next(0, 10);

        }
        return str;
    }
    public bool ChangePassword(string oldPass, string newPassword)
    {
        if (oldPass == Password)
        {
            Password = newPassword;
            return true;

        }
        return false;
    }
    public DateTime SetExpiry()
    {
        return DateTime.UtcNow.AddYears(5);


    }
    public string GenerateCVV2()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 4; i++)
        {
            str += rnd.Next(0, 10);
        }
        return str;
    }
    public string GenerateCartNumber()
    {
        var strArr = new string[4];
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                str += rnd.Next(0, 10);

            }
            strArr[i] = str;
            str = "";
        }
        return string.Join(" ", strArr);
    }
    public string GenerateAccountNumber()
    {
        var strArr = new string[3];
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 2; i++)
        {
            str += rnd.Next(0, 10);
        }
        strArr[0] = str;
        str = "";
        str += this.UserId;
        for (int i = 0; i < 4; i++)
        {
            str += rnd.Next(0, 10);

        }
        strArr[1] = str;
        str = "";
        if (this.Type == 1)
        {
            str += 1;
        }
        else
        {
            str += 2;

        }
        strArr[2] = str;


        return string.Join(".", strArr);
    }
    public static Account OpenAccount(int type, double amount, string userId)
    {
        if (type  == 1)
        {
            return new Account(1, amount, userId);
        }
        else
        {
            return new Account(2, amount, userId);

        }
        
    }

}