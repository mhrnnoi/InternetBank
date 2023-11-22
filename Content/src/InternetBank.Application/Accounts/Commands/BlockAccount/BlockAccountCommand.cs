using ErrorOr;
using MediatR;

namespace InternetBank.Application.Accounts.Commands.BlockAccount;

public record BlockAccountCommand(string AccountId, string UserId) :IRequest<ErrorOr<bool>>;