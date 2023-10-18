using MediatR;

namespace InternetBank.Application.Features.Account.Commands.UnBlockAccount;

public record UnBlockAccountCommand(int Id):IRequest<bool>;