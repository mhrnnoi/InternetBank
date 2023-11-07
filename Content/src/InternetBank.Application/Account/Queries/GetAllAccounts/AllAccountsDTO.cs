namespace InternetBank.Application.Account.Queries.GetAllAccounts;

public record AllAccountsDTO(string AccountNumber,
                             int Id,
                             string CardNumber);