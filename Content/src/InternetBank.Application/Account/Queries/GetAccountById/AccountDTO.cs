using InternetBank.Domain.Accounts;

namespace InternetBank.Application.Account.Queries.GetAccountById;

public record AccountDTO(string AccountNumber,
                         string CardNumber,
                         string Cvv2,
                         string ExpiryYear,
                         string ExpiryMonth,
                         string StaticPassword,
                         int Id,
                         AccountTypes AccountType);
