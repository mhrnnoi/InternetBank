using InternetBank.Domain.Accounts;

namespace InternetBank.Api.Requests.Accounts;

public record CreateAccountRequest(double Amount,
                                   int AccountTypes,
                                   string UserId);