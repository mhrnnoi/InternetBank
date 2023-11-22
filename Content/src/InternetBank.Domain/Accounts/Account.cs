using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Enums;
using InternetBank.Domain.Accounts.Events;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Transactions.Entities;
using InternetBank.Domain.Transactions.Enums;
using InternetBank.Domain.ValueObjects;

namespace InternetBank.Domain.Accounts.Entities;


public sealed class Account : AggregateRoot<AccountId>
{
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
    public AccountTypes AccountType { get; private set; }
    public double Amount { get; private set; }
    public AccountNumber AccountNumber { get; private set; }
    public CardNumber CardNumber { get; private set; }
    public string UserId { get; init; }
    public Cvv2 Cvv2 { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public Password StaticPassword { get; private set; }
    public bool IsBlocked { get; private set; }

    private Account(AccountId accountId,
                    AccountTypes accountType,
                    double amount,
                    string userId,
                    AccountNumber accountNumber,
                    CardNumber cardNumber,
                    Cvv2 cvv2,
                    Password staticPassword,
                    DateTime expiryDate
                    ) : base(accountId)
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

#pragma warning disable CS8618
    private Account()
    {
    }
#pragma warning restore CS8618

    public ErrorOr<string> TransferMoney(double amount,
                                         Cvv2 cvv2,
                                         DateTime expiryDate,
                                         Otp otp,
                                         Account destinationAccount)
    {
        Description description;
        var transaction = _transactions.FirstOrDefault(x => x.Amount == amount
                                                            && x.DestinationCardNumber == destinationAccount.CardNumber);
        if (transaction is null)
        {
            var transactionOrError = Transaction.CreateTransaction(amount, CardNumber);
            if (transactionOrError.IsError)
                return transactionOrError.Errors;

            transaction = transactionOrError.Value;

            description = transaction.ChangeDescription(DescriptionTypes.IncorrectPass);
            _transactions.Add(transaction);
            return description.Value;
        }
        if (cvv2 != Cvv2)
            return Errors.Transaction.IncorrectCVV2;

        if (expiryDate != ExpiryDate)
            return Errors.Transaction.IncorrectExpiryDate;

        if ((otp != transaction.Otp) || (DateTime.UtcNow > otp.OtpExpireDate))
        {
            description = transaction.ChangeDescription(DescriptionTypes.IncorrectPass);
            return description.Value;
        }
        if (IsBlocked)
        {
            description = transaction.ChangeDescription(DescriptionTypes.BlockedSourceAccount);
            return description.Value;
        }

        if (destinationAccount.IsBlocked)
        {
            description = transaction.ChangeDescription(DescriptionTypes.BlockedDestinationAccount);
            return description.Value;
        }
        if (Amount < amount)
        {
            description = transaction.ChangeDescription(DescriptionTypes.LowBalance);
            return description.Value;
        }
        description = transaction.ChangeDescription(DescriptionTypes.Success);
        Withdrawl(amount);
        destinationAccount.Deposit(amount);
        return description.Value;
    }
    public ErrorOr<TransactionId> SendOTP(double amount,
                                          CardNumber destinationCardNumber)
    {


        var transaction = _transactions.FirstOrDefault(x => x.Status == Status.Pending
                                                            && x.Amount == amount
                                                            && destinationCardNumber == x.DestinationCardNumber);
        if (transaction is null)
        {
            var transactionOrError = Transaction.CreateTransaction(amount, destinationCardNumber);
            if (transactionOrError.IsError)
                return transactionOrError.Errors;

            transaction = transactionOrError.Value;
            _transactions.Add(transaction);
            return transaction.Id;
        }

        return transaction.SendOtp();



    }

    public static ErrorOr<Account> OpenAccount(int type,
                                               double amount,
                                               string userId)
    {
        List<Error> errors = new();
        if (type is 1 || type is 2)
        {
            var accType = (AccountTypes)type;
            if (amount >= 10000)
            {
                var accountId = AccountId.GenerateId();
                var accountNumber = AccountNumber.GenerateAccountNumber(userId, accType);
                var cardNumber = CardNumber.GenerateCartNumber();
                var cvv2 = Cvv2.GenerateCVV2();
                var staticPassword = Password.GeneratePassword();
                var expiryDate = DateTime.UtcNow.AddYears(5);


                var account = new Account(accountId,
                                          accType,
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

    public string Balance()
    {
        return "" + Amount + "\n" + Id + "\n" + AccountNumber;
    }
    public double Deposit(double amount)
    {
        Amount += amount;
        return Amount;
    }
    public ErrorOr<double> Withdrawl(double amount)
    {
        if (amount > Amount)
            return Errors.Transaction.InsuficcentBalance;

        Amount -= amount;

        return Amount;
    }

    public void BlockAccount()
    {
        IsBlocked = true;
    }
    public void UnBlockAccount()
    {
        IsBlocked = false;
    }

    public ErrorOr<bool> ChangePassword(Password oldPass,
                                        Password newPassword,
                                        Password repeatNewPassword)
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


}