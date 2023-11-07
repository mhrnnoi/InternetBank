namespace InternetBank.Application.Account.Queries.GetAccountBalanceById;

public record BalanceDTO(double Amount,
                         int Id,
                         string AccountNumber);