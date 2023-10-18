namespace InternetBank.Application.Features.Account.Commands.CreateAccount;

public record CreateAccountResult(string AccountNumber,
string CardNumber,
string CVV2,
DateTime ExireDate,
string StaticPassword,
int Id,
int AccountType);
