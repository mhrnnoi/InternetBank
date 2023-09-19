namespace InternetBank.Domain.Accounts;

public class Account
{
    public int Id { get; set; }
    public AccountTypes Type { get; private set; }
    public double Amount { get; private set; }
    public string Number { get; private set; }
    public string CardNumber { get; private set; }
    public int UserId { get; init; }
    public string CVV2 { get; private set; }
    public string ExpiryDate { get; private set; }
    public string Password { get; private set; }
    public bool IsBlocked { get; private set; }
    private Account(AccountTypes type, double amount, int userId)
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
            str += rnd.Next(0, 9);

        }
        return str;
    }
    public int ChangePassword(string oldPass, string newPassword)
    {
        if (oldPass == Password)
        {
            Password = newPassword;
            return 1;

        }
        return 0;
    }
    public string SetExpiry()
    {
        var str = "";
        if ((DateTime.UtcNow.Year + 5).ToString().Length >= 2)
        {
            str += (DateTime.UtcNow.Year + 5).ToString().TakeLast(2);

        }
        else
        {
            str += 0;
            str += (DateTime.UtcNow.Year + 5);
        }
        str += "/";
        if (DateTime.UtcNow.Month.ToString().Length >= 2)
        {
            str += DateTime.UtcNow.Month;
        }
        else
        {
            str += 0;
            str += DateTime.UtcNow.Month;
        }
        return str;

    }
    public string GenerateCVV2()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 4; i++)
        {
            str += rnd.Next(0, 9);
        }
        return str;
    }
    public string GenerateCartNumber()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 1; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                str += rnd.Next(0, 9);

            }
            str += ".";
        }
        for (int j = 0; j < 4; j++)
        {
            str += rnd.Next(0, 9);

        }
        return str;
    }
    public string GenerateAccountNumber()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 2; i++)
        {
            str += rnd.Next(0, 9);
        }
        str += ".";
        str += this.UserId;
        for (int i = 0; i < 4; i++)
        {
            str += rnd.Next(0, 9);

        }
        str += ".";

        if (this.Type == AccountTypes.Saving)
        {
            str += 1;
        }
        else
        {
            str += 2;

        }

        return str;
    }
    public static Account OpenAccount(AccountTypes type, double amount, int userId)
    {
        return new Account(type, amount, userId);
    }

}