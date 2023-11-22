namespace InternetBank.Application.Accounts.Queries.GetAccountById;

public record AccountDTO(string AccountNumber,
                         string CardNumber,
                         string Cvv2,
                         string ExpiryYear,
                         string ExpiryMonth,
                         string StaticPassword,
                         Guid Id,
                         string AccountType);
