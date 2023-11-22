using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Enums;
using InternetBank.Domain.Accounts.Events;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.ValueObjects;
using Newtonsoft.Json;

namespace InternetBank.Domain.Accounts.Entities;


public sealed class Account : AggregateRoot
{
    public int AccountType { get; private set; }
    public double Amount { get; private set; }
    public string AccountNumber { get; private set; }
    public string CardNumber { get; private set; }
    public string UserId { get; init; }
    public string Cvv2 { get; private set; }
    public string ExpiryYear { get; private set; } = null!;
    public string ExpiryMonth { get; private set; } = null!;
    public string StaticPassword { get; private set; }
    public bool IsBlocked { get; private set; }

    [JsonConstructor]
    private Account(int accountType,
                    double amount,
                    string userId,
                    string accountNumber,
                    string cardNumber,
                    string cvv2,
                    string staticPassword,
                    string expiryYear,
                    string expiryMonth)
    {

        AccountType = accountType;
        Amount = amount;
        UserId = userId;
        IsBlocked = false;
        AccountNumber = accountNumber;
        CardNumber = cardNumber;
        Cvv2 = cvv2;
        StaticPassword = staticPassword;
        ExpiryYear = expiryYear;
        ExpiryMonth = expiryMonth;
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

    public static ErrorOr<bool> BlockAccount(Account? account)
    {
        if (account is null)
            return Errors.Account.AccountIsNotYours;
        account.IsBlocked = true;
        return true;
    }
    public static ErrorOr<bool> UnBlockAccount(Account? account)
    {
        if (account is null)
            return Errors.Account.AccountIsNotYours;
        account.IsBlocked = false;
        return true;
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
    public static ErrorOr<bool> ChangePassword(Password oldPass,
                                               Password newPassword,
                                               RepeatPassword repeatNewPassword,
                                               Account? account)
    {
        if (account is null)
            return Errors.Account.AccountIsNotYours;
        if (oldPass.Value == account.StaticPassword)
        {
            if (newPassword.Value == repeatNewPassword.Value)
            {
                if (newPassword.Value.Length is 6 && newPassword.Value.All(char.IsDigit))
                {
                    account.StaticPassword = newPassword.Value;
                    return true;
                }
                else
                    return Errors.Account.IncorrectPassFormat;
            }
            else
                return Errors.Account.PassAndRepeatPassIsNotSame;
        }
        else
            return Errors.Account.IncorrectPass;
    }
    private static DateTime SetExpiry()
    {
        return DateTime.UtcNow.AddYears(5);
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
    private static string GenerateAccountNumber(string userId, AccountTypes accountType)
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
        var strUserId = userId.ToString();
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

        if (accountType == AccountTypes.Saving)
            str += 1;

        else
            str += 2;

        strArr[2] = str;
        return string.Join(".", strArr);
    }
    public static ErrorOr<Account> OpenAccount(int type,
                                               double amount,
                                               string userId)
    {
        List<Error> errors = new();
        if (type is 1 || type is 2)
        {
            if (amount > 10000)
            {
                var accountNumber = GenerateAccountNumber(userId, (AccountTypes)type);
                var cardNumber = GenerateCartNumber();
                var cvv2 = GenerateCVV2();
                var staticPassword = GeneratePassword();
                var expiryDate = SetExpiry();


                var account = new Account(type,
                                   amount,
                                   userId,
                                   accountNumber,
                                   cardNumber,
                                   cvv2,
                                   staticPassword,
                                   expiryDate.Year.ToString(),
                                   expiryDate.Month.ToString());
                account.AddDomainEvent(new AccountCreatedDomainEvent(account));
                return account;
            }
            errors.Add(Errors.Account.MinimumAccountAmount);
            return errors;

        }
        errors.Add(Errors.Account.InvalidAccountType);
        return errors;
    }


}