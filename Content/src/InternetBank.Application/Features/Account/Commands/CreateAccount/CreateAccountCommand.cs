using MediatR;

namespace InternetBank.Application.Features.Account.Commands.CreateAccount;

public record CreateAccountCommand(double Amount, int AccountTypes, string UserId) : IRequest<CreateAccountResult>;
