using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Enums;
using InternetBank.Domain.Accounts.Events;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Transactions.Entities;
using InternetBank.Domain.Transactions.Enums;
using InternetBank.Domain.ValueObjects;
using Newtonsoft.Json;

namespace InternetBank.Domain.Accounts.Entities;

public sealed class Account 
{
    public string Id { get; set; }
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
    public AccountTypes AccountType { get; private set; }
    public double Amount { get; private set; }
    // public AccountNumber AccountNumber { get; private set; }
    // public CardNumber CardNumber { get; private set; }
    // public Cvv2 Cvv2 { get; private set; }
    // public ExpiryDate ExpiryDate { get; private set; } = null!;
    // public Password StaticPassword { get; private set; }
    public bool IsBlocked { get; private set; }
    public string UserId { get; init; }

    [JsonConstructor]
    private Account(string id
                    , AccountTypes accountType,
                    double amount,
                    string userId)
    {

        AccountType = accountType;
        Amount = amount;
        IsBlocked = false;
        // AccountNumber =AccountNumber.GenerateAccountNumber(userId, accountType)  ;//accountNumber
        // CardNumber = CardNumber.GenerateCartNumber();// cardNumber
        // Cvv2 = Cvv2.GenerateCVV2();
        // StaticPassword = Password.GeneratePassword();
        // ExpiryDate = ExpiryDate.SetExpiry();
        UserId = userId;
        Id = id;
    }

    // public Transaction? FindTransaction(double amount,
    //                                    CardNumber destinationCardNumber)
    // {
    //     var transaction = _transactions.FirstOrDefault(x => x.Status == Status.Pending
    //                                         && amount == x.Amount
    //                                         && x.DestinationCardNumber == destinationCardNumber);

    //     return transaction;
    // }
    // public TransactionId AddTransaction(Transaction transaction)
    // {
    //     _transactions.Add(transaction);
    //     return transaction.Id;
    // }

    // public string TransferMoney(double amount,
    //                             Cvv2 cvv2,
    //                             ExpiryDate expiryDate,
    //                             Otp otp,
    //                             Account destAccount,
    //                             Transaction transaction)
    // {

    //     // if (expiryDate != ExpiryDate || cvv2 != Cvv2) //for security reason
    //     // {
    //     //     transaction.ChangeDescription("عملیات ناموفق - رمز نادرست ");
    //     //     transaction.ChangeStatus(Status.Failed);
    //     //     return transaction.Description;
    //     // }
    //     if (transaction.Otp is null || DateTime.UtcNow > transaction.OtpExpireDate || otp != transaction.Otp)
    //     {
    //         transaction.ChangeDescription("عملیات ناموفق - رمز نادرست");
    //         transaction.ChangeStatus(Status.Failed);
    //         return transaction.Description;
    //     }
    //     if (IsBlocked)
    //     {
    //         transaction.ChangeDescription("عملیات ناموفق - اکانت مبدا مسدود هست");
    //         transaction.ChangeStatus(Status.Failed);
    //         return transaction.Description;

    //     }

    //     if (destAccount.IsBlocked)
    //     {
    //         transaction.ChangeDescription("عملیات ناموفق - اکانت مقصد مسدود هست");
    //         transaction.ChangeStatus(Status.Failed);

    //         return transaction.Description;

    //     }

    //     if (Amount < amount)
    //     {
    //         transaction.ChangeDescription("عملیات ناموفق - عدم موجودی");
    //         transaction.ChangeStatus(Status.Failed);

    //         return transaction.Description;

    //     }
    //     transaction.ChangeStatus(Status.Success);
    //     transaction.ChangeDescription("عملیات موفق");

    //     Withdrawl(amount);
    //     destAccount.Deposit(amount);
    //     return transaction.Description;
    // }

    // public string Balance()
    // {
    //     //id
    //     return "" + Amount + "\n"  + "\n" + AccountNumber;
    // }
    private void Deposit(double amount)
    {
        Amount += amount;
    }
    private void Withdrawl(double amount)
    {
        Amount -= amount;
    }

    public void BlockAccount()
    {
        IsBlocked = true;
    }
    public void UnBlockAccount()
    {

        IsBlocked = false;
    }

    // public ErrorOr<bool> ChangePassword(Password oldPass,
    //                                     Password newPassword,
    //                                     Password repeatNewPassword)
    // {
    //     if (oldPass == StaticPassword)
    //     {
    //         if (newPassword == repeatNewPassword)
    //         {
    //             StaticPassword = newPassword;
    //             return true;
    //         }
    //         else
    //             return Errors.Account.PassAndRepeatPassIsNotSame;
    //     }
    //     else
    //         return Errors.Account.IncorrectPass;
    // }


    public static ErrorOr<Account> OpenAccount(int type,
                                               double amount,
                                               string userId)
    {
        if (type is 1 || type is 2)
        {
            if (amount > 10000)
            {

                var accountType = (AccountTypes)type;
                // var accountNumber = AccountNumber.GenerateAccountNumber(userId, accountType);
                // var cardNumber = CardNumber.GenerateCartNumber();
                // var cvv2 = Cvv2.GenerateCVV2();
                // var staticPassword = Password.GeneratePassword();
                // var expiryDate = ExpiryDate.SetExpiry();
                var accId = "AccountId.GenerateId()";

                var account = new Account(accId,
                accountType,
                                          amount,
                                          userId);

                // account.AddDomainEvent(new AccountCreatedDomainEvent(account));
                return account;
            }
            return Errors.Account.MinimumAccountAmount;
        }
        return Errors.Account.InvalidAccountType;
    }




}