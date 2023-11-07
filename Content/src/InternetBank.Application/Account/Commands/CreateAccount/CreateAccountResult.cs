using InternetBank.Domain.Accounts;

namespace InternetBank.Application.Account.Commands.CreateAccount;

public record CreateAccountResult(string AccountNumber,
                                  string CardNumber,
                                  string Cvv2,
                                  string ExpiryYear,
                                  string ExpiryMonth,
                                  string StaticPassword,
                                  int Id,
                                  AccountTypes AccountType);
