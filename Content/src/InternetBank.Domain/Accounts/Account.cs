using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Enums;
using InternetBank.Domain.Accounts.Events;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Transactions.Entities;
using InternetBank.Domain.ValueObjects;
using Newtonsoft.Json;

namespace InternetBank.Domain.Accounts.Entities;

public sealed class Account : AggregateRoot<AccountId>
{
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
    public AccountTypes AccountType { get; private set; }
    public double Amount { get; private set; }
    public AccountNumberVO AccountNumber { get; private set; }
    public CardNumberVO CardNumber { get; private set; }
    public string UserId { get; init; }
    public Cvv2VO Cvv2 { get; private set; }
    public ExpiryDateVO ExpiryDate { get; private set; } = null!;
    public PasswordVO StaticPassword { get; private set; }
    public bool IsBlocked { get; private set; }

    [JsonConstructor]
    private Account(AccountId accountId,
                    AccountTypes accountType,
                    double amount,
                    string userId,
                    AccountNumberVO accountNumber,
                    CardNumberVO cardNumber,
                    Cvv2VO cvv2,
                    PasswordVO staticPassword,
                    ExpiryDateVO expiryDate) : base(accountId)
    {

        AccountType = accountType;
        Amount = amount;
        UserId = userId;
        IsBlocked = false;
        AccountNumber = accountNumber;
        CardNumber = cardNumber;
        Cvv2 = cvv2;
        StaticPassword = staticPassword;
        ExpiryDate = expiryDate;
    }


    public ErrorOr<TransactionId> SetOTP(CardNumberVO destinationAccountCardNumber,
                                         double amount,
                                         string otp,
                                         Cvv2VO cvv2VO,
                                         ExpiryDateVO expiryDateVO)
    {
        if (cvv2VO != Cvv2)
            return Errors.Transaction.IncorrectCVV2;

        if (expiryDateVO != ExpiryDate)
            return Errors.Transaction.IncorrectExpiryDate;

        var transaction = _transactions.FirstOrDefault(x => x.Amount == amount
                                                            && x.DestinationCardNumber == destinationAccountCardNumber
                                                            && x.IsSuccess == false && x.SourceCardNumber == CardNumber);

        if (transaction is not null)
        {
            transaction.SetOtp(otp);
            return transaction.Id;
        }
        var transactionCreateRes = CreateTransaction(amount, destinationAccountCardNumber);
        if (transactionCreateRes.IsError)
            return transactionCreateRes.Errors;

        _transactions.Add(transactionCreateRes.Value);
        transactionCreateRes.Value.SetOtp(otp);
        return transactionCreateRes.Value.Id;
    }
    


    public string Balance()
    {
        return "" + Amount + "\n" + Id + "\n" + AccountNumber;
    }
    private void Deposit(double amount, Transaction transaction)
    {
        Amount += amount;
        _transactions.Add(transaction);

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

    public ErrorOr<bool> ChangePassword(PasswordVO oldPass,
                                        PasswordVO newPassword,
                                        PasswordVO repeatNewPassword)
    {
        if (oldPass == StaticPassword)
        {
            if (newPassword == repeatNewPassword)
            {
                StaticPassword = newPassword;
                return true;
            }
            else
                return Errors.Account.PassAndRepeatPassIsNotSame;
        }
        else
            return Errors.Account.IncorrectPass;
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
                var accountNumber = AccountNumberVO.GenerateAccountNumber(userId, type);
                var cardNumber = CardNumberVO.GenerateCartNumber();
                var cvv2 = Cvv2VO.GenerateCVV2();
                var staticPassword = PasswordVO.GeneratePassword();
                var expiryDate = ExpiryDateVO.SetExpiry();


                var account = new Account(AccountId.GenerateId(),
                                          (AccountTypes)type,
                                          amount,
                                          userId,
                                          accountNumber,
                                          cardNumber,
                                          cvv2,
                                          staticPassword,
                                          expiryDate);

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