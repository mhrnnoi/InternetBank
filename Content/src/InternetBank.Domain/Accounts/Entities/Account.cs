using InternetBank.Domain.Abstracts.Entity.Primitives;
using InternetBank.Domain.Accounts.Enums;
using InternetBank.Domain.Exceptions.Account;

namespace InternetBank.Domain.Accounts.Entities;

public sealed class Account : Entity
{
    public AccountTypes AccountType { get; private set; }
    public double Amount { get; private set; }
    public string AccountNumber { get; private set; }
    public string CardNumber { get; private set; }
    public string UserId { get; init; }
    public string Cvv2 { get; private set; }
    public string ExpiryYear { get; private set; } = null!;
    public string ExpiryMonth { get; private set; } = null!;
    public string StaticPassword { get; private set; }
    public bool IsBlocked { get; private set; }
    private Account(AccountTypes accountType,
                    double amount,
                    string userId)
    {

        AccountType = accountType;
        Amount = amount;
        UserId = userId;
        IsBlocked = false;
        AccountNumber = GenerateAccountNumber();
        CardNumber = GenerateCartNumber();
        Cvv2 = GenerateCVV2();
        StaticPassword = GeneratePassword();
        SetExpiry();
    }
    public string Balance()
    {
        return "" + Amount + "\n" + Id + "\n" + AccountNumber;
    }
    public void Deposit(double amount)
    {
        Amount += amount;
    }
    public void Withdrawl(double amount)
    {
        Amount -= amount;
    }
    public string Report()
    {
        return "" + StaticPassword;
    }
    public void BlockAccount()
    {
        IsBlocked = true;
    }
    public void UnBlockAccount()
    {
        IsBlocked = false;
    }
    private static string GeneratePassword()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 6; i++)
        {
            str += rnd.Next(0, 10);

        }
        return str;
    }
    public bool ChangePassword(string oldPass,
                               string newPassword,
                               string repeatNewPassword)
    {
        if (oldPass == StaticPassword)
        {
            if (newPassword == repeatNewPassword)
            {
                if (newPassword.Length is 6 && newPassword.All(char.IsDigit))
                {
                    StaticPassword = newPassword;
                    return true;
                }
                else
                    throw new IncorrectPassFormat();
            }
            else
                throw new PassAndRepeatPassIsNotSame();
        }
        else
            throw new IncorrectPass();
    }
    private void SetExpiry()
    {
        ExpiryYear = DateTime.UtcNow.AddYears(5).Year
                                    .ToString();
        ExpiryMonth = DateTime.UtcNow.AddYears(5).Month
                                     .ToString();
    }
    private static string GenerateCVV2()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 4; i++)
        {
            str += rnd.Next(0, 10);
        }
        return str;
    }
    private static string GenerateCartNumber()
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
        return string.Join("", strArr);
    }
    private string GenerateAccountNumber()
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
        var strUserId = UserId.ToString();
        for (int i = 0; i < 3; i++)
        {
            str += strUserId[i];
        }

        for (int i = 0; i < 4; i++)
        {
            str += rnd.Next(0, 10);
        }

        strArr[1] = str;
        str = "";

        if (AccountType == AccountTypes.Saving)
            str += 1;

        else
            str += 2;

        strArr[2] = str;
        return string.Join(".", strArr);
    }
    public static Account OpenAccount(int type,
                                      double amount,
                                      string userId)
    {
        if (type is 1 || type is 2)
            return new Account((AccountTypes)type, amount, userId);

        else
            throw new InvalidAccountType();
    }

}