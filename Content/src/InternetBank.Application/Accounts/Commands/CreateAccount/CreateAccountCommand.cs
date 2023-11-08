using MediatR;

namespace InternetBank.Application.Accounts.Commands.CreateAccount;

public record CreateAccountCommand(double Amount,
                                   int AccountType,
                                   string UserId) : IRequest<CreateAccountResult>;
