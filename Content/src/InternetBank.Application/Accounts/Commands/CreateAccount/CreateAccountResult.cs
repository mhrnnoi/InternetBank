using InternetBank.Domain.Accounts.Enums;

namespace InternetBank.Application.Accounts.Commands.CreateAccount;

public record CreateAccountResult(string AccountNumber,
                                  string CardNumber,
                                  string Cvv2,
                                  string ExpiryYear,
                                  string ExpiryMonth,
                                  string StaticPassword,
                                  string Id,
                                  AccountTypes AccountType);
