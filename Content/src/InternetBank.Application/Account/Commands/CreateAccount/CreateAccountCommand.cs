using MediatR;

namespace InternetBank.Application.Account.Commands.CreateAccount;

public record CreateAccountCommand(double Amount,
                                   int AccountType,
                                   string UserId) : IRequest<CreateAccountResult>;
