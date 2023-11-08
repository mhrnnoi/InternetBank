using InternetBank.Domain.Accounts.Enums;

namespace InternetBank.Application.Accounts.Queries.GetAccountById;

public record AccountDTO(string AccountNumber,
                         string CardNumber,
                         string Cvv2,
                         string ExpiryYear,
                         string ExpiryMonth,
                         string StaticPassword,
                         string Id,
                         AccountTypes AccountType);
