using MediatR;

namespace InternetBank.Application.Account.Commands.UnBlockAccount;

public record UnBlockAccountCommand(int Id):IRequest<bool>;