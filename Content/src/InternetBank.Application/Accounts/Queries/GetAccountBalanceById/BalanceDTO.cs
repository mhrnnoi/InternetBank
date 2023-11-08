namespace InternetBank.Application.Accounts.Queries.GetAccountBalanceById;

public record BalanceDTO(double Amount,
                         string Id,
                         string AccountNumber);