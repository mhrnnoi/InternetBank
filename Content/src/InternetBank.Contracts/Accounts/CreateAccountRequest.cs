namespace InternetBank.Contracts.Requests.Accounts;

public record CreateAccountRequest(double Amount,
                                   int AccountType);