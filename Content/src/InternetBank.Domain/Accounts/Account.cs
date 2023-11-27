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

    public List<Transaction> GetTransactionsByDateAndSuccess(DateOnly? from,
                                                             DateOnly? to,
                                                             bool? isSuccess)
    {
        return Transactions.Where(x => DateOnly.FromDateTime(x.CreatedDateTime) >= (from ??= DateOnly.FromDateTime(x.CreatedDateTime))
                                          && DateOnly.FromDateTime(x.CreatedDateTime) <= (to ??= DateOnly.FromDateTime(x.CreatedDateTime))
                                          && x.IsSuccess == (isSuccess ??= x.IsSuccess)).ToList();
    }

    public ErrorOr<string> TransferMoney(double amount,
                                         Cvv2 cvv2,
                                         DateTime expiryDate,
                                         Otp otp,
                                         Account destinationAccount)
    {
        Description? description = null;
        var transaction = _transactions.FirstOrDefault(x => x.Amount == amount
                                                            && x.DestinationCardNumber == destinationAccount.CardNumber
                                                            && x.Status == Status.Pending);
        Transaction transaction2;
        if (transaction is null)
        {
            var transactionOrError = Transaction.CreateTransaction(amount, CardNumber, otp);
            if (transactionOrError.IsError)
                return transactionOrError.Errors;

            transaction = transactionOrError.Value;
            description = transaction.ChangeDescription(DescriptionTypes.IncorrectPass);
            _transactions.Add(transaction);
            return description.Value;
        }
        transaction2 = Transaction.CreateTransaction(amount, destinationAccount.CardNumber, otp).Value;
        if (cvv2 != Cvv2)
            description = transaction2.ChangeDescription(DescriptionTypes.IncorrectCvv2);

        if (expiryDate.Year != ExpiryDate.Year || expiryDate.Month != ExpiryDate.Month)
            description = transaction2.ChangeDescription(DescriptionTypes.IncorrectExpiryDate);

        if ((transaction.Otp is null) || (otp != transaction.Otp) || (DateTime.UtcNow > transaction.OtpExpireDate.Value))
            description = transaction2.ChangeDescription(DescriptionTypes.IncorrectPass);

        if (IsBlocked)
            description = transaction2.ChangeDescription(DescriptionTypes.BlockedSourceAccount);

        if (destinationAccount.IsBlocked)
            description = transaction2.ChangeDescription(DescriptionTypes.BlockedDestinationAccount);

        if (Amount < amount)
            description = transaction2.ChangeDescription(DescriptionTypes.LowBalance);

        if (description is not null)
        {
            _transactions.Add(transaction2);
            return description.Value;
        }
        description = transaction.ChangeDescription(DescriptionTypes.Success);
        Withdrawl(amount);
        destinationAccount.Deposit(amount, transaction);
        return description.Value;
    }
    public ErrorOr<Otp> SendOTP(double amount,
                                CardNumber destinationCardNumber)
    {


        var transaction = _transactions.FirstOrDefault(x => x.Status == Status.Pending
                                                            && x.Amount == amount
                                                            && destinationCardNumber == x.DestinationCardNumber);
        if (transaction is null)
        {
            var otp = Otp.GenerateOTP();
            var transactionOrError = Transaction.CreateTransaction(amount, destinationCardNumber, otp);
            if (transactionOrError.IsError)
                return transactionOrError.Errors;

            transaction = transactionOrError.Value;
            _transactions.Add(transaction);
            return otp;
        }

        var transacitonOrError = transaction.SendOtp();
        if (transacitonOrError.IsError)
            return transacitonOrError.Errors;

        return transacitonOrError.Value.Otp;



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
        return "" + Amount + "\n" + Id.Value + "\n" + AccountNumber.Value;
    }
    public double Deposit(double amount, Transaction transaction)
    {
        if (transaction.DestinationCardNumber != CardNumber)
            _transactions.Add(transaction);

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