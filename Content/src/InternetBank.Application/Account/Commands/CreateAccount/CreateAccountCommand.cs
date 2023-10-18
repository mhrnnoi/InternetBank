using MediatR;

namespace InternetBank.Application.Account.Commands.CreateAccount;

public record CreateAccountCommand(double Amount, int AccountTypes, string UserId) : IRequest<CreateAccountResult>;
