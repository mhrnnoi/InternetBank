namespace InternetBank.Api.Requests.Accounts;

public record CreateAccountRequest(double Amount,
                                   int AccType,
                                   string UserId);