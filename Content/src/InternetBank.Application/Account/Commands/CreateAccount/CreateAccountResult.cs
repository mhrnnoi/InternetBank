using InternetBank.Domain.Accounts;

namespace InternetBank.Application.Account.Commands.CreateAccount;

public record CreateAccountResult(string AccountNumber,
                                  string CardNumber,
                                  string CVV2,
                                  string ExpireDate,
                                  string StaticPassword,
                                  int Id,
                                  AccountTypes AccountType);
